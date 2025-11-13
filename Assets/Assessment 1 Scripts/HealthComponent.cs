using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<float, float, float> OnDamageTaken; // current health, max health, damage amount
    public event Action<MonoBehaviour> OnDead;

    [SerializeField] private int MaxHealth;
    private int CurrentHealth;

    private void Awake()
    {   CurrentHealth = MaxHealth;  }

    public void TakeDamage(int damage, MonoBehaviour causer)
    {
        float change = Math.Min(CurrentHealth, damage);

        CurrentHealth -= damage;
        OnDamageTaken?.Invoke(CurrentHealth, MaxHealth, change);

        if (CurrentHealth <= 0)
            OnDead?.Invoke(causer);
    }
}
