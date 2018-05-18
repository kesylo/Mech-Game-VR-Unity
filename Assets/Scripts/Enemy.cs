using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 50f;

    Transform target;

    public float speed = 10f;

    public float distance = 100f;

    public float retreatDistance = 100f;

    float timeBtwShots;

    public float startTimeBtwShots;

    public GameObject EnemybulletObject;

    public GameObject muzzleParticle;

    public Transform bulletStartPosition;

    ParticleSystem particle;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        particle = muzzleParticle.GetComponent<ParticleSystem>();
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, target.position) < distance && Vector3.Distance(transform.position, target.position) > retreatDistance)
        {
            // stop moving
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }


        if (timeBtwShots <= 0)
        {
            particle.Play();
            Instantiate(EnemybulletObject, bulletStartPosition.transform.position, Quaternion.identity);  // Quaternion.identity = no rotation
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    public void Takedamage(float amountOfDamage)
    {
        health -= amountOfDamage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
