using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topRotation : MonoBehaviour {

    #region public vaiables
    public float angle = 360;
    public float RotationLeft = -1.1f;
    public float RotationRight = 1.1f;
    #endregion


    private string hipsTurn = "HipsTurn";

    void Update()
    {
        float RotateAxis = Input.GetAxis(hipsTurn);

        ApplyInput(RotateAxis);



        if (RotationLeft > 0)
        {
            RotationLeft *= -1;
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(0, RotationLeft * angle * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(0, RotationRight * angle * Time.deltaTime, 0);
        }
    }

    void Turn(float input)
    {
        transform.Rotate(0, input * angle * Time.deltaTime, 0);
    }

    void ApplyInput( float RotateAxis)
    {
        Turn(RotateAxis);
    }
}