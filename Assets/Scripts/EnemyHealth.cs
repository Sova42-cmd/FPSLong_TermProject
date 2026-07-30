using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public GameObject deathEffect;

    // Event triggered when health hits 0(ne znayu chto eto)
    public event Action OnDeath; 

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
        //let enemy spawner know that enemy died
        OnDeath?.Invoke();

        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnEnemyKilled();
        }

        Destroy(gameObject);
    }
}