using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    bool isDead;
    bool damaged;

    public UnityEvent<int> onHealthChanged;

    void Awake ()
    {
        currentHealth = startingHealth;
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        onHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

       

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

}
