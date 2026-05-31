using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}