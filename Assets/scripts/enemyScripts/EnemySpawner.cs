using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private int currentLocationIndex; //keeps track of current spawn location
    private int currentGroupSize;

    void Start()
    {
        GenerateWave();
        spawnInterval = 5.0f; // Set the spawn interval to 5 seconds.
        currentLocationIndex = 0; // Start at the first spawn location.
    }

    void Update()
    {

        if (enemiesToSpawn.Count > 0 || waveDuration < Time.deltaTime)
        {
            if (spawnTimer <= 0)
            {
                // Spawn the first enemy in the list.
                Instantiate(enemiesToSpawn[0], spawnLocations[currentLocationIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                currentGroupSize += 1;

                if (currentGroupSize % 2 == 0) // Check if we've spawned a pair of enemies.
                {
                    currentLocationIndex = (currentLocationIndex + 1) % spawnLocations.Count; // Rotate through spawn locations.
                }

                // Reset the spawn timer.
                spawnTimer = spawnInterval;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
        else
        {
            currWave += 1;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        // Start the wave timer.
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();

        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
            enemies[randEnemyId].enemyPrefab.GetComponent<EnemyAI>().enemyCost = randEnemyCost;
            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
