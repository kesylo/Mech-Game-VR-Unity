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

    public GameObject enemyLeftHand;

    Animator shootAnim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        particle = muzzleParticle.GetComponent<ParticleSystem>();

        timeBtwShots = startTimeBtwShots;

        shootAnim = enemyLeftHand.GetComponent<Animator>();
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
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalkingFront", true);
                anim.SetBool("isShooting", true);
                Shoot();
                shootAnim.SetBool("isShootingAnim", true);
            }
            else
            {
                anim.SetBool("isWalkingFront", false);
                anim.SetBool("isShooting", true);
                Shoot();
                shootAnim.SetBool("isShootingAnim", true);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingFront", false);
            anim.SetBool("isShooting", false);
            shootAnim.SetBool("isShootingAnim", false);
        }
        
    }

    public void Takedamage(float amountOfDamage)
    {
        health -= amountOfDamage;
        if (health <= 0f)
        {
            StartCoroutine(EnemyDie());
        }
    }


    IEnumerator EnemyDie()
    {
        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(1.0f);
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
