using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField]
    public int currentHealth;

    public bool refillOnStart = false;

    private void Start()
    {
        if (refillOnStart)
        {
            currentHealth = maxHealth;
        }
    }

    public bool ReceiveDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Death();
            return true;
        }
        return false;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
