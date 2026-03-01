using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Hazard Settings")]
    public int damage = 1;          // Damage dealt to player
    public bool instantDeath = false; // Optional: kills player instantly

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            if (health != null)
            {
                if (instantDeath)
                    health.Die();
                else
                    health.TakeDamage(damage);
            }
        }
    }
}