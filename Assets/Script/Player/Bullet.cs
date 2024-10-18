using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;  // The amount of damage this bullet deals
    public float bulletLifetime = 5f;  // The time after which the bullet is destroyed, even if it hasn't hit anything

    void Start()
    {
        // Destroy the bullet after a certain time, regardless of collision
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits something with a collider
        if (collision.collider.CompareTag("Enemy"))
        {
            // Get the EnemyHealth component on the object the bullet collided with
            EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exists, deal damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, collision.contacts[0].point);
            }

            // Destroy the bullet after hitting an enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet after hitting any other object (optional, you can decide what to do here)
            Destroy(gameObject);
        }
    }
}
