using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform player; 

    public float spawnInterval = 3f;
    public float spawnDistance = 10f;

    private GameObject currentEnemy;

    void Start()
    {
        InvokeRepeating("TrySpawn", 1f, spawnInterval);
    }

    void TrySpawn()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, spawnPoint.position);

        if (currentEnemy == null && distance < spawnDistance)
        {
            currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Enemy Spawned!");
        }
    }
}