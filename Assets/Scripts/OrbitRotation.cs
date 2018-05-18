using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotation : MonoBehaviour {


    public Transform orbitCenter;

    public float speed = 10f;

    void FixedUpdate()
    {
        // go right
        if (Input.GetKey(KeyCode.L))
        {
                transform.RotateAround(orbitCenter.transform.position, Vector3.up, speed * Time.deltaTime);
        }

        // go Left
        if (Input.GetKey(KeyCode.K))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.down, speed * Time.deltaTime);
        }

        // go up
        if (Input.GetKey(KeyCode.I))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.left, speed * Time.deltaTime);
        }
        
        // go down
        if (Input.GetKey(KeyCode.O))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.right, speed * Time.deltaTime);
        }
    }
}
