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

    static Animator anim;

    public float minDistance = 60;

    ParticleSystem particle;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        particle = muzzleParticle.GetComponent<ParticleSystem>();
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {

        if (Vector3.Distance (target.position, this.transform.position) < 100)
        {
            Vector3 direction = target.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            if (direction.magnitude > minDistance)
            {
                this.transform.Translate(0, 0, 0.1f);
                anim.SetBool("isWalkingFront", true);
                anim.SetBool("isShooting", true);
                Shoot();
            }
            else
            {
                anim.SetBool("isWalkingFront", false);
                anim.SetBool("isShooting", true);
                Shoot();
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingFront", false);
            anim.SetBool("isShooting", false);
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

    void Shoot()
    {
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
}
