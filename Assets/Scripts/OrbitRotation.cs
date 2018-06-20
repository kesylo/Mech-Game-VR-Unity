using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotation : MonoBehaviour {
    public Transform orbitCenter;
    public float speed = 10f;

    void Update()
    {
        float moveVerticalAxis = Input.GetAxis("D-Vertical");
        float moveHorizontalAxis = Input.GetAxis("D-Horizontal");

        if (moveVerticalAxis > 0 || Input.GetKey(KeyCode.O))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.left, speed * Time.deltaTime);
        }
        if (moveVerticalAxis < 0 || Input.GetKey(KeyCode.I))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.right, speed * Time.deltaTime);
        }
        if (moveHorizontalAxis > 0 || Input.GetKey(KeyCode.K))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        if (moveHorizontalAxis < 0 || Input.GetKey(KeyCode.L))
        {
            transform.RotateAround(orbitCenter.transform.position, Vector3.down, speed * Time.deltaTime);
        }


    }

    //public float rotateSpeed = 20.0f;
    //public float angleMax = 30.0f;
    //public Transform target;

    //private Vector3 initialVectorY = Vector3.forward;
    //private Vector3 initialVectorX = Vector3.forward;



    //// Use this for initialization
    //void Start()
    //{

    //    if (target != null)
    //    {
    //        initialVectorY = transform.position - target.position;
    //        initialVectorY.y = 0;

    //        initialVectorX = transform.position - target.position;
    //        initialVectorX.y = 0;
    //    }

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    float moveVerticalAxis = Input.GetAxis("D-Vertical");
    //    float moveHorizontalAxis = Input.GetAxis("D-Horizontal");

    //    if (target != null)
    //    {
    //        float rotateDegreesY = 0f;

    //        float rotateDegreesX = 0f;

    //        if (moveHorizontalAxis > 0)
    //        {
    //            rotateDegreesY += rotateSpeed * Time.deltaTime;
    //        }
    //        if (moveHorizontalAxis < 0)
    //        {
    //            rotateDegreesY -= rotateSpeed * Time.deltaTime;
    //        }
    //        ///////////////
    //        if ( moveVerticalAxis > 0)
    //        {
    //            rotateDegreesX += rotateSpeed * Time.deltaTime;
    //        }
    //        if ( moveVerticalAxis < 0)
    //        {
    //            rotateDegreesX -= rotateSpeed * Time.deltaTime;
    //        }

    //        Vector3 currentVectorY = transform.position - target.position;

    //        Vector3 currentVectorX = transform.position - target.position;

    //        currentVectorY.y = 0;

    //        currentVectorX.x = 0;

    //        float angleBetweenY = Vector3.Angle(initialVectorY, currentVectorY) * (Vector3.Cross(initialVectorY, currentVectorY).y > 0 ? 1 : -1);

    //        float angleBetweenX = Vector3.Angle(initialVectorX, currentVectorX) * (Vector3.Cross(initialVectorX, currentVectorX).y > 0 ? 1 : -1);

    //        float newAngleY = Mathf.Clamp(angleBetweenY + rotateDegreesY, -angleMax + rotateDegreesY, angleMax + rotateDegreesY);

    //        float newAngleX = Mathf.Clamp(angleBetweenX + rotateDegreesX, -17, 0);

    //        rotateDegreesY = newAngleY - angleBetweenY;

    //        rotateDegreesX = newAngleX - angleBetweenX;


    //        transform.RotateAround(target.localPosition, Vector3.up, rotateDegreesY);

    //        transform.RotateAround(target.localPosition, Vector3.right, -rotateDegreesX);
    //    }

    //}


}
