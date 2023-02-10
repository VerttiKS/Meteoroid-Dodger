using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBehavior : MonoBehaviour
{
    //Fuels Rigidbody
    private Rigidbody fuelRB;

    //Torque
    private float maxTorque = 5;

    //Spawn position
    private float xPosMax = 2;
    private float yPos = 1.25f;
    private float zPos = 2;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn position
        transform.position = RandomPos();

        //Get Rigidbody
        fuelRB = GetComponent<Rigidbody>();

        //Spin the fuel
        fuelRB.AddTorque(0, RandomTorqueFuel(), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -3)
        {
            Destroy(gameObject);
        }
    }

    float RandomTorqueFuel()
    {
        return (Random.Range(-maxTorque, maxTorque) * 100);
    }

    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xPosMax, xPosMax), yPos, zPos);
    }
}
