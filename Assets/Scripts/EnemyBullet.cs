using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float BulletSpeed;

    Transform player;

    Vector3 target;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y + 15, player.position.z);
    }

	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, BulletSpeed * Time.deltaTime);

        // check if bullet reaches destination
        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            DestroyBullet();
        }
	}

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // destroy bullet when collide with player
    void OnTriggerEnter( Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyBullet();
        }
        else
        {
            DestroyBullet();
        }
    }
}
