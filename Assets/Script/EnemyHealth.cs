using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;

    CapsuleCollider capsuleCollider;

    bool isDead;

    void Awake()
    {
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>(); // Added to initialize the collider reference
    }

    void Update()
    {
        // You can remove this if there's no specific need for Update
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true; // Make sure the collider is set correctly when dead

        FindObjectOfType<ScoreManager>().AddScore(scoreValue); // Score increment

        Destroy(gameObject, 2f); // Destroy the enemy object after 2 seconds
    }
}
