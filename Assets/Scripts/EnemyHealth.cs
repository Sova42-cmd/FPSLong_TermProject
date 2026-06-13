using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public GameObject deathEffect;

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
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        LevelManager.Instance.OnEnemyKilled();
        Destroy(gameObject);
    }
}