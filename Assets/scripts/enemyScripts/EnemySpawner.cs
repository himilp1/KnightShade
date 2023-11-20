using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<Enemy> bossEnemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();
    private CurrentWaveText currentWaveText;
    public GameObject HUD;
    public NavMeshSurface surface;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private int currentLocationIndex; //keeps track of current spawn location
    private int currentGroupSize;
    private int waveGroupSize;
    void Start()
    {
        currentWaveText = HUD.GetComponent<CurrentWaveText>();
        GenerateWave();
        spawnInterval = 1.0f; // Set the spawn interval to 5 seconds.
        currentLocationIndex = 0; // Start at the first spawn location.
        waveGroupSize = 2;
    }

    void Update()
    {
        GameObject [] enemyCount = GameObject.FindGameObjectsWithTag("enemy");
        if (enemiesToSpawn.Count > 0)
        {
            if (spawnTimer <= 0)
            {
                // Spawn the first enemy in the list.
                Instantiate(enemiesToSpawn[0], spawnLocations[currentLocationIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                currentGroupSize += 1;
                //surface.BuildNavMesh();

                if (currentGroupSize % waveGroupSize == 0) // Check if we've spawned a pair of enemies.
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
            if (enemyCount.Length <= 0) 
            {
                currWave += 1;
                waveGroupSize += 1;
                currentWaveText.SetWave(currWave);
                GenerateWave();
            }
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        if (currWave % 3 == 0)
        {
            waveValue += 10;
        }
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

        if (currWave % 3 == 0)
        {
            int randEnemyCost = enemies[3].cost;
            generatedEnemies.Add(enemies[3].enemyPrefab);
            waveValue -= randEnemyCost;
            // bossText = HUD.GetComponent<BossText>();

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