using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeImproved : MonoBehaviour 
{
    public Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth; 
    
    
    [Header("=== Attack ===")]
    public float damage = 10f;
    public float attackCooldown = 1.5f;
    public float attackRange = 1.5f;
    private float attackTimer = 0f;

    [Header("=== Separation ===")]
    public float separationRadius = 2.0f; //how close they get to each other
    public float separationWeight = 1.5f; //how strongly they push away from each other
    public LayerMask enemyLayer; //check only for enemies for optimization

    [Header("=== VFX ===")]
    public GameObject hitEffect;

    private NavMeshAgent agent;
    private readonly Collider[] nearbyEnemies = new Collider[10]; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        
        enemyHealth = GetComponent<EnemyHealth>();
        
        agent.stoppingDistance = attackRange;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 targetPosition = player.position;

        Vector3 separationForce = CalculateSeparationForce();
        targetPosition += separationForce * separationWeight;

        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPosition);
        }
        else
        {
            agent.isStopped = true;
            HandleAttack();
        }
    }

    private Vector3 CalculateSeparationForce()
    {
        Vector3 separation = Vector3.zero;
        int neighborCount = 0;

        int numFound = Physics.OverlapSphereNonAlloc(transform.position, separationRadius, nearbyEnemies, enemyLayer);

        for (int i = 0; i < numFound; i++)
        {
            Collider neighbor = nearbyEnemies[i];
            if (neighbor.gameObject == this.gameObject) continue;

            Vector3 awayFromNeighbor = transform.position - neighbor.transform.position;
            float distance = awayFromNeighbor.magnitude;

            if (distance > 0)
            {
                separation += awayFromNeighbor.normalized / distance;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            separation /= neighborCount;
        }

        return separation;
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;
        
        if (attackTimer <= 0f)
        {
            if (playerHealth != null)
            {
                GameObject effect = Instantiate(hitEffect, player.position, transform.rotation);
                Destroy(effect, 1f);
                playerHealth.TakeDamage(damage);
            }
            attackTimer = attackCooldown;
        }
    }

    private void OnDrawGizmosSelected() //krasnenkiy gizmos/show seperation radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}