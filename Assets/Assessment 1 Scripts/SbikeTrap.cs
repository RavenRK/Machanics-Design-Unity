using UnityEngine;

public class SbikeTrap : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent<HealthComponent>(out var HealthComponent))
        {
            HealthComponent.TakeDamage(damageAmount, this);
        }
    }
}
