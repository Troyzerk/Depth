using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    //Player//
    public string race = "Goblin";

    //Health//
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float healthConsumption = 0f;

    public Coroutine healthConsumtionCoroutine;

    //Movement//
    public float baseSpeed = 0f;


    //Script Refs//
    private PlayerDeath playerDeath;
    private HealthBarUI healthBarUI;


    public void Start()
    {

        playerDeath = gameObject.AddComponent<PlayerDeath>();
        healthBarUI = GameObject.FindWithTag("HUD").GetComponent<HealthBarUI>();
        UpdateMaxHealth();
    }

    public IEnumerator HealthConsumtionOverTime()
    {

        //while health is above 0 decrease the health of the player. when the health of the player reaches 0 kill the player
        while (currentHealth > 0)
        {
            float damage = healthConsumption * Time.deltaTime;
            currentHealth -= damage;
            healthBarUI.SetHealth(currentHealth);
            yield return null;
        }

        playerDeath.killPlayer();

    }
    public void AddHealth(float health)
    {
        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
            healthBarUI.SetHealth(currentHealth);
        }
    }
    public void RemoveHealth(float damage)
    {
        if (currentHealth < damage)
        {
            playerDeath.killPlayer();
        }
        else
        {
            currentHealth -= damage;
            healthBarUI.SetHealth(currentHealth);
        }
    }
    public void UpdateMaxHealth()
    {
        healthBarUI.SetMaxHealth(maxHealth);
    }
}
