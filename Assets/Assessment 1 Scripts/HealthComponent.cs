using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int MaxHealth;
    private int CurrentHealth;
    private bool isDead = false;

    private void Awake()
    {   CurrentHealth = MaxHealth;  }

    public void TakeDamage(int damage)
    {
        CurrentHealth += damage;

        if (CurrentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDead();
        }
    }
    virtual public void OnDead()
    {
        if (isDead)
        {
            Debug.Log("Character is Dead");
            // Add additional death handling logic here
        }
    }
    void Update()
    {
        
    }
}
