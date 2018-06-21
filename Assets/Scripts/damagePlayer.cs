using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damagePlayer : MonoBehaviour {

    float currentHealth;
    public float maxHealth;
    public float damageAmount;
    public Slider healthBar;

    void Start () {
        currentHealth = maxHealth;

        healthBar.value = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float CalculateHealth ()
    {
        return currentHealth / maxHealth;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            //print("hit");
            //playerHealth -= damage;
            ReduceHealth(damageAmount);
        }
    }

    private void ReduceHealth(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.value = CalculateHealth();

        if (currentHealth <= 0)
        {
            FindObjectOfType<GameManagerScript>().EndGame();
        }
    }
}
