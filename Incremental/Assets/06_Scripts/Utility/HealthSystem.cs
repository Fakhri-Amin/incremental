using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class HealthSystem : MonoBehaviour
{

    public Action OnHealthReset;
    public Action OnHealthChanged;
    public Action OnFirstHealthDepleted;
    public Action OnDead;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int healthAmount;

    private bool isDead = false;

    void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        SetHealth(maxHealth);
        isDead = false;
    }

    public void SetHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthAmount = maxHealth;

        OnHealthReset?.Invoke();
        OnHealthChanged?.Invoke();
    }

    [Button]
    public void Damage(int damageAmount)
    {
        if (isDead) return; // ✅ Already dead, ignore

        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        OnHealthChanged?.Invoke();

        if (healthAmount <= 0)
        {
            isDead = true;
            healthAmount = 0;
            OnDead?.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead) isDead = false;

        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        OnHealthChanged?.Invoke();
    }


    public int GetHealth()
    {
        return (int)healthAmount;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / maxHealth;
    }

    public void HandleFirstHealthDepleted()
    {
        OnFirstHealthDepleted?.Invoke();
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        healthAmount = Mathf.Min(amount, maxHealth);
    }

    public bool IsHealthFull() => healthAmount == maxHealth;
    public bool IsHealthZero() => healthAmount <= 0;
}
