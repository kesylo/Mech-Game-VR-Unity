using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour {


    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float fireRate = 0.3f;
   
	public float damage = 10f;

    private float timer;

	public float range = 100f;

    public GameObject impactEffect;

    [SerializeField]
	private Transform bulletStartPoint;

    [SerializeField]
    float impactForce = 60f;

    public GameObject muzzleParticle;

    ParticleSystem particle;


    void Start()
    {
        particle = muzzleParticle.GetComponent<ParticleSystem>();
    }


    void Update () {

        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                fire();
                FindObjectOfType<AudioManager>().play("BasicGun");
            }
        }
	}


    void fire ()
    {
        particle.Play();
        
		RaycastHit hit;

		//Debug.DrawLine (bulletStartPoint.transform.position, bulletStartPoint.transform.forward, Color.red, 2f);
		if (Physics.Raycast(bulletStartPoint.transform.position, bulletStartPoint.transform.forward, out hit, range))
		{
			// we enter here if we hit something
			// let's display the name of the object hited
			Debug.Log(hit.transform.name);
			Enemy target = hit.transform.GetComponent<Enemy> ();
			// make sure we hit and object with a target
			if (target != null) {
				target.Takedamage (damage);
			}

            // shake objects on hit
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // dust from hit point
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);

		}

    }
}
