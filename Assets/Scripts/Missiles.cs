using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System;

public class Missiles : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private float fireRate = 6f;

    public float damage = 50f;

    private float timer;

    public float range = 100f;

    public GameObject impactEffect;

    RaycastHit hit;

    [SerializeField]
    private Transform bulletStartPoint;

    [SerializeField]
    float impactForce = 700f;

    public GameObject muzzleParticle;

    ParticleSystem particle;

    public float radius = 5f;

    public float explosionForce = 700f;




    void Start()
    {
        particle = muzzleParticle.GetComponent<ParticleSystem>();
    }


    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            if (Input.GetKey(KeyCode.B))
            {
                timer = 0f;
                Explode();
            }
        }
    }



    private void Explode()
    {
        FindObjectOfType<AudioManager>().play("MissileShoot");
        particle.Play();

        if (Physics.Raycast(bulletStartPoint.transform.position, bulletStartPoint.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.Takedamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                FindObjectOfType<AudioManager>().play("MissileHit");
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            FindObjectOfType<AudioManager>().play("MissileHit");
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1);

            // get nearbear objects
            ExplosionForce();

            Destroy(impact, 4f);

        }
    }

    private void ExplosionForce()
    {
        Collider[] colidersInRadius = Physics.OverlapSphere(hit.transform.position, radius);
        foreach (Collider nearbyObject in colidersInRadius)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, hit.transform.position, radius);
            }
        }
    }
}
