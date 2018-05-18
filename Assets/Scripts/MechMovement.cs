using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
  
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveVerticalAxis = Input.GetAxis("Vertical");
        float moveHorizontalAxis = Input.GetAxis("Horizontal");

        if (moveVerticalAxis > 0)
        {
			// FindObjectOfType<AudioManager>().play("MechWalk");
            anim.SetBool("isWalkingFront", true);
			anim.SetBool("isTurningRight", false);
            anim.SetBool("isWalkingBack", false);
			anim.SetBool("isTurningLeft", false);
            anim.SetBool("isIdle", false);
            //Debug.Log("vertical pos");
        }
        else if (moveVerticalAxis < 0)
        {
			anim.SetBool("isWalkingBack", true);
			anim.SetBool("isWalkingFront", false);
			anim.SetBool("isTurningRight", false);
			anim.SetBool("isTurningLeft", false);
			anim.SetBool("isIdle", false);
           // Debug.Log("vertical neg");
        }
        else if (moveVerticalAxis == 0)
        {
			anim.SetBool("isWalkingBack", false);
			anim.SetBool("isWalkingFront", false);
			anim.SetBool("isTurningRight", false);
			anim.SetBool("isTurningLeft", false);
			anim.SetBool("isIdle", true);
        }


		/*--------------------------------------------------------------------*/

		if (moveHorizontalAxis > 0)
		{
			anim.SetBool("isTurningRight", true);
			anim.SetBool("isWalkingFront", false);
			anim.SetBool("isTurningLeft", false);
			anim.SetBool("isWalkingBack", false);
			anim.SetBool("isIdle", false);
			//Debug.Log("vertical pos");
		}
		else if (moveHorizontalAxis < 0)
		{
			anim.SetBool("isTurningLeft", true);
			anim.SetBool("isWalkingFront", false);
			anim.SetBool("isWalkingBack", false);
			anim.SetBool("isTurningRight", false);
			anim.SetBool("isIdle", false);
			// Debug.Log("vertical neg");
		}

        
    }


   /* void OnCollisionEnter (Collision col)
    {
        if (col.collider.tag == "Floor")
        {
            Debug.Log("floor colided");
        }
    }*/
}