using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{

    //GameManager
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Set GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.back * Time.deltaTime * gameManager.speed;

    }
}
