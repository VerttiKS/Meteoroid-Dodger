using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //PlayerMovementScript
    public PlayerMovement playerMovement;

    //GameObjects
    public GameObject[] meteoroidPrefabs;
    public GameObject[] fuelPrefab;

    //Game Active
    public bool gameActive;
    public bool moveZ = true;

    //Game speed
    public float speed = 0;

    //Meteoroid spawn time
    private float minSpawnSec = 1;
    private float maxSpawnSec = 5;

    //Fuel spawn time
    private float minFuelSpawnSec = 10;
    private float maxFuelSpawnSec = 50;

    //Difficulty
    public int scoreDifficulty = 1; //1 = mix. 5 = max.
    private int scoreDifficultyMax = 5;
    private int difficulty;
    private int[] difficultyPoints = {0, 2000, 10000, 40000, 80000, 80000};

    //Fuel
    private int fuel;

    //Score
    private int score;

    //Title
    public GameObject title;

    //UI of the game
    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI scoreText;

    //Game Over screen
    public GameObject gameOverScreen;
    public Button restartButton;

    //Music
    private AudioSource musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Get PlayerMovement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //Get music player
        musicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int setDifficulty)
    {
        //Turns off the title
        title.SetActive(false);

        //Activates the game
        gameActive = true;

        //Makes the things move
        moveZ = true;

        //Set Speed to 1
        speed = 1;

        //Set Difficulty
        difficulty = setDifficulty;

        //Speed based of difficulty
        speed *= difficulty;

        //Set fuel to 100
        fuel = 100;
        scoreText.text = "Score:" + score;

        //Create texts
        fuelText.text = "Fuel:" + fuel + "%";

        //Countdown Fuel
        StartCoroutine(FuelCountdown());

        //Spawn Meteoroids
        StartCoroutine(SpawnMeteoroid());

        //Spawn fuels
        StartCoroutine(SpawnFuel());

        //Start motor particles
        playerMovement.motor.Play();

        //Play music
        musicPlayer.Play();
    }

    public void StopGame()
    {
        //Stops the game
        gameActive = false;

        //Stops Z from moving
        StartCoroutine(ReduceSpeed());

        //Stop music
        musicPlayer.Stop();

        //Stop player motor
        playerMovement.motor.Stop();

        //Game over screen
        gameOverScreen.SetActive(true);
    }

    //Spawns Fuel
    IEnumerator SpawnFuel()
    {
        while(gameActive)
        {
            float timeIndexFuel = Random.Range(minFuelSpawnSec, maxFuelSpawnSec);
            yield return new WaitForSeconds(timeIndexFuel);

            Instantiate(fuelPrefab[0]);
        }
    }

    //Makes fuel go down
    IEnumerator FuelCountdown()
    {

        while(fuel > 0 && gameActive)
        {
            yield return new WaitForSeconds(1.5f);
            fuel--;
            fuelText.text = "Fuel:" + fuel + "%";
        }

        if (fuel <= 0)
        {
            StopGame();
        }
    }

    //Fills fuel
    public void FillFuel()
    {
        fuel = 100;
        fuelText.text = "Fuel:" + fuel + "%";
    }

    //Spawns Meteoroids
    IEnumerator SpawnMeteoroid()
    {
        while(gameActive)
        {
            float timeIndex = Random.Range(minSpawnSec, maxSpawnSec) / (scoreDifficulty * difficulty);
            yield return new WaitForSeconds(timeIndex);

            int meteoroidIndex = Random.Range(0, meteoroidPrefabs.Length);
            Instantiate(meteoroidPrefabs[meteoroidIndex]);
        }
    }

    //Score
    public void AddScore(int scoreAmount)
    {
        //Add the scoreAmount to the current score
        score += scoreAmount;

        //Update the scoreText
        scoreText.text = "Score:" + score;

        if (score > difficultyPoints[scoreDifficulty] && scoreDifficulty < scoreDifficultyMax) 
        {
            //Debugging
            Debug.Log("////////////////DIFFICULTY CHANGE///////////////");
            Debug.Log("Array before the change: " + difficultyPoints[scoreDifficulty]);
            Debug.Log("Difficulty before the change: " + scoreDifficulty);

            //Increase score difficulty
            scoreDifficulty++;

            //More Debugging
            Debug.Log("////////////////////////////////////////////////");
            Debug.Log("Array after the change: " + difficultyPoints[scoreDifficulty]);
            Debug.Log("Difficulty after the change: " + scoreDifficulty);
            Debug.Log("////////////////////////////////////////////////");
        }
    }

    //Makes the speed go down when the game ends
    IEnumerator ReduceSpeed()
    {
        while(speed > 0)
        {
            speed -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        while(speed < 0)
        {
            speed = 0;
        }
    }

    //Restart game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
