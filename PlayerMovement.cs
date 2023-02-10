using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float thrust = 5;
    public float playerSpeed = 1.5f;

    private Quaternion normalRotation = Quaternion.Euler(0, 0, 0);
    private Vector3 playerPosition;
    private bool dashCooldown = true; //When true, you can use dash

    //Ship Gameobject
    public GameObject ship;

    //Particle
    public ParticleSystem motor;
    public ParticleSystem explosion;
    public ParticleSystem shipExplosion;

    //GameManager
    private GameManager gameManager;

    //Boundary
    private float xRange = 2;

    //Sounds
    private AudioSource soundPlayer;
    public AudioClip explosionSFX;
    public AudioClip fuelSFX;

    // Start is called before the first frame update
    void Start()
    {
        //Set GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Set Player's Rigidbody
        playerRb = GetComponent<Rigidbody>();

        //Set Audiosource
        soundPlayer = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //Set MaxAngularVelocity
        playerRb.maxAngularVelocity = 12.5f;
    }

    // Update is called once per frame
     void LateUpdate()
    {
        if (gameManager.gameActive)
        {
            //Checks player position
            playerPosition = transform.position;

            //Listens to player controls
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            //Player control left and right
            transform.position += Vector3.right * Time.deltaTime * horizontalInput * playerSpeed;

            //Player dash
            if (Input.GetKeyDown(KeyCode.Space) && dashCooldown)
            {
                playerRb.AddForce(Vector3.right * thrust * horizontalInput, ForceMode.Impulse);

                StartCoroutine(Rotate(Vector3.back * horizontalInput));

                StartCoroutine(DashCooldownTimer());
            }

            //Right Boundary
            if (playerPosition.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }

            //Left Boundary
            if (playerPosition.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
        }

        if (!gameManager.gameActive)
        {
            playerRb.angularVelocity = Vector3.zero;
            transform.rotation = normalRotation;
        }
    }

    IEnumerator Rotate(Vector3 rotationDirection)
    {
        transform.rotation = normalRotation;
        playerRb.AddTorque(rotationDirection * thrust, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        if (dashCooldown == true){ 
        playerRb.angularVelocity = Vector3.zero;
        transform.rotation = normalRotation;
        };
    }

    IEnumerator DashCooldownTimer()
    {
        dashCooldown = false;

        yield return new WaitForSeconds(0.3f);

        //Fix position bug
        transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);

        dashCooldown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.gameActive)
        {
            ship.SetActive(false);

            //Play explosion effects
            soundPlayer.PlayOneShot(explosionSFX);
            explosion.Play();
            shipExplosion.Play();

            //End the game
            gameManager.StopGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Fill your fuel
        gameManager.FillFuel();

        //Adds 50 to score
        gameManager.AddScore(50);

        //Plays a cool sound effects
        soundPlayer.PlayOneShot(fuelSFX);

        //Destroys fuel
        Destroy(other.gameObject);
    }
}
