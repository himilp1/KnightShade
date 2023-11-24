using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();
    public List<Transform> chosenSpawns = new List<Transform>();
    private CurrentWaveText currentWaveText;
    public GameObject HUD;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private int currentLocationIndex; //keeps track of current spawn location
    private int currentGroupSize;
    private int waveGroupSize;
    public NavMeshSurface surface;
    private GameObject player;
    private StatTracker statTracker;

    private int activatedSpawns;

    void Start()
    {
        currentWaveText = HUD.GetComponent<CurrentWaveText>();
        GenerateWave();
        spawnInterval = 1.0f; // Set the spawn interval to 5 seconds.
        currentLocationIndex = 0; // Start at the first spawn location.
        waveGroupSize = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        activatedSpawns = 3; ;
        statTracker = player.GetComponent<StatTracker>();
        surface = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("enemy");
        if (enemiesToSpawn.Count > 0)
        {
            if (spawnTimer <= 0)
            {
                PickSpawnLocations();
                // Spawn the first enemy in the list.
                Instantiate(enemiesToSpawn[0], chosenSpawns[currentLocationIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                currentGroupSize += 1;
                if(enemiesToSpawn.Count == 0){
                    surface.BuildNavMesh();
                }
                if (currentGroupSize % waveGroupSize == 0) // Check if we've spawned a pair of enemies.
                {
                    currentLocationIndex = (currentLocationIndex + 1) % chosenSpawns.Count; // Rotate through spawn locations.
                    Debug.Log("currentLocationIndex: " + currentLocationIndex);
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
                statTracker.SurvivedWave();
                GenerateWave();
            }
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        // Start the wave timer.
        waveTimer = waveDuration;
    }

    public void PickSpawnLocations()
    {
        List<Location> possibleSpots = new List<Location>();

        //float distance = Vector3.Distance(agent.transform.position, player.position);
        foreach (Transform spawnLocation in spawnLocations)
        {
            float distance = Vector3.Distance(spawnLocation.position, player.transform.position);
            Location currentLocation = new Location
            {
                location = spawnLocation,
                distance = distance
            };
            possibleSpots.Add(currentLocation);
        }
        possibleSpots.Sort((x, y) => x.distance.CompareTo(y.distance));

        for (int i = 0; i < activatedSpawns; i++)
        {
            chosenSpawns.Add(possibleSpots[i].location);
        }

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

[System.Serializable]
public class Location
{
    public Transform location;
    public float distance;
}