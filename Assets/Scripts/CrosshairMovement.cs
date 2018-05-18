using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour {


	void Update () {

		//var height = canvas.transform.position.y;
		//Debug.Log (height);

		float dpadVertical = Input.GetAxis("DpadVertical");
		float dpadHorizontal = Input.GetAxis("DpadHorizontal");


		if (dpadVertical > 0)
		{
			//right
			//transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
			transform.Rotate(0, -1.1f * 360 * Time.deltaTime, 0);
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

	}
}
