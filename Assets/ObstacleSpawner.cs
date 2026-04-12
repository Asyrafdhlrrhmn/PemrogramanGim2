using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnTime = 2f;

    public float groundY = -3f; 

    float[] lanes = new float[] { -2f, 0f, 2f };

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnTime);
    }

    void SpawnObstacle()
    {
        int randomLane = Random.Range(0, lanes.Length);
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);

        Vector3 spawnPos = new Vector3(
            lanes[randomLane] + 10f, 
            groundY,                
            0
        );

        Instantiate(obstaclePrefabs[randomObstacle], spawnPos, Quaternion.identity);
    }
}
