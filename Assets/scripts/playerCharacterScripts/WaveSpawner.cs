using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Assign your enemy prefab in the inspector
    public Transform[] spawnPoints;      // Assign spawn points where enemies should appear
    public float timeBetweenWaves = 5f;  // Time to wait between waves
    public int enemiesPerWave = 5;       // Number of enemies to spawn per wave

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)  // Infinite waves. Modify this condition if you want a maximum number of waves
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);  // Optional: slight delay between each enemy spawn within a wave
            }

            currentWave++;
        }
    }

    void SpawnEnemy()
    {
        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
