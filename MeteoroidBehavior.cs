using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroidBehavior : MonoBehaviour
{
    //GameManager
    private GameManager gameManager;

    //Rigidbody of Meteoroids
    private Rigidbody meteoRB;

    //Torque
    private float maxTorque = 2;

    //Spawn position
    private float xPosMax = 2;
    private float yPos = 1.25f;
    private float zPos = 2;

    //Size
    private float sizeMin = 0.5f;
    private float sizeMax = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Set GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Size
        float scale = RandomSize();
        transform.localScale = new Vector3(scale, scale, scale);

        //Spawn position
        transform.position = RandomPos();

        //Getting Rigidbody
        meteoRB = GetComponent<Rigidbody>();

        //Making the object spin
        meteoRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -3)
        {
            gameManager.AddScore(100);
            Destroy(gameObject);
        }
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xPosMax, xPosMax), yPos, zPos);
    }

    private float RandomSize()
    {
        return Random.Range(sizeMin, sizeMax);
    }
}
