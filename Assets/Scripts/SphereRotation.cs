using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotation : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 1.1f;

	void Update () {
        float dpadVertical = Input.GetAxis("DpadVertical");
        float dpadHorizontal = Input.GetAxis("DpadHorizontal");


        if (dpadVertical > 0)
        {
            //right
            //transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            transform.Rotate(0, rotationSpeed * 0.01f * Time.deltaTime, 0);
        }
        else if (dpadVertical < 0)
        {
            //left
            transform.Rotate(0, -1.1f * 360 * Time.deltaTime, 0);
        }



        if (dpadHorizontal > 0)
        {
            // up
            transform.Rotate(0, 1.1f * 360 * Time.deltaTime, 0);
        }
        else if (dpadHorizontal < 0)
        {
            //down
            transform.Rotate(0, 1.1f * 360 * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
