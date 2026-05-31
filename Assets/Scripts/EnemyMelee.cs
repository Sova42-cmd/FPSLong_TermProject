using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public Transform player;
    public float damage = 10f;
    public float attackCooldown = 1.5f;
    public float attackRange = 1.5f;

    private NavMeshAgent agent;
    private PlayerHealth playerHealth;
    private float attackTimer = 0f;

    public GameObject hitEffect;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        agent.SetDestination(player.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                GameObject effect = Instantiate(hitEffect, player.position, transform.rotation);
                Destroy(effect, 1f);
                playerHealth.TakeDamage(damage);
                attackTimer = attackCooldown;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }
}