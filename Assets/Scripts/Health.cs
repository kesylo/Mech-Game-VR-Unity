
using UnityEngine;

public class Health : MonoBehaviour {

    public int startingHealth = 5;
    private int currentHealth = 5;

    void OnEnable () {
        currentHealth = startingHealth;
    }

    public void TakeDamage (int damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
	}

    private void Die()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {

    }
}
