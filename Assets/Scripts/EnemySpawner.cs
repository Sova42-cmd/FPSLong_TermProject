using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("=== Spawn Config ===")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxActiveEnemies = 8;

    //[Header("=== VFX / Effects ===")]
    //public GameObject spawnVfxPrefab;
    //public float vfxPreDelay = 0.5f; // Delay between VFX and actual enemy spawn

    private float timer = 0f;
    private int currentActiveEnemies = 0;

    void Update()
    {
        // Stop spawning if the level goal is already reached
        if (LevelManager.Instance != null && LevelManager.Instance.IsGoalReached())
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            if (currentActiveEnemies < maxActiveEnemies)
            {
                StartCoroutine(SpawnSequence());
            }
        }
    }

    private IEnumerator SpawnSequence()
    {
        if (spawnPoints.Length == 0) yield break;

        // Pick a random spawn point
        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn warning/telegraph VFX
        //if (spawnVfxPrefab != null)
        //{
            //GameObject vfx = Instantiate(spawnVfxPrefab, chosenPoint.position, chosenPoint.rotation);
        //    Destroy(vfx, 2f);
        //}

        //yield return new WaitForSeconds(vfxPreDelay);

        // Spawn actual enemy
        GameObject enemyObj = Instantiate(enemyPrefab, chosenPoint.position, chosenPoint.rotation);
        currentActiveEnemies++;

        // Track when enemy dies to free up active capacity
        EnemyHealth health = enemyObj.GetComponent<EnemyHealth>();
        if (health != null)
        {
            // Register an event or callback when this enemy dies
            health.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath()
    {
        currentActiveEnemies--;
    }
}