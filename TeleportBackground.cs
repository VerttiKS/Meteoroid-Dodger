using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBackground : MonoBehaviour
{

    private Vector3 startPos = new Vector3(0, 0, 13.5f);
    private float endSpotZ = -6.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= endSpotZ)
        {
            transform.position = startPos;
        }
    }
}
