using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject ratPrefab;
    [SerializeField] GameObject goblinPrefab;
    [SerializeField] GameObject skeletonPrefab;

    [Header("Spawn Positions")]
    [SerializeField]
    List<Vector3> spawnPositions = new List<Vector3> {
        new Vector3(0f, 1f, -1f),
        new Vector3(-4.5f, 1f, -1f),
        new Vector3(4.5f, 1f, -1f)
    };

    [Header("Spawn Chance")]
    [SerializeField] float uncommonSpawnChance = 0.35f;
    [SerializeField] float rareSpawnChance = 0.15f;

    [Header("Multi-Spawn Chance")]
    [SerializeField] float doubleSpawnChance = 0.35f;
    [SerializeField] float tripleSpawnChance = 0.15f;

    public void Spawn(bool initialSpawn = false)
    {
        int spawnCount = RandomizeNumberOfSpawns(initialSpawn);
        GameManager.Instance.SetSpawnCount(spawnCount);
        for (int i = 0; i < spawnCount; ++i)
        {
            Vector3 spawnPosition = spawnPositions[i];
            GameObject enemyPrefab = SelectRandomEnemy();
            Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Enemy>();
            GameManager.Instance.AddEnemy(enemy);

            int levelDiff = (initialSpawn) ? 0 : (int)Player.Instance.LVL - 1;
            enemy.LevelUp(levelDiff);
        }
    }

    GameObject SelectRandomEnemy()
    {
        GameObject enemy;
        float enemyRoll = UnityEngine.Random.value;
        if (enemyRoll < rareSpawnChance)
        {
            enemy = skeletonPrefab;
        }
        else if (enemyRoll < uncommonSpawnChance)
        {
            enemy = goblinPrefab;
        }
        else
        {
            enemy = ratPrefab;
        }
        return enemy;
    }

    int RandomizeNumberOfSpawns(bool initialSpawn = false)
    {
        int spawns;
        float spawnRoll = UnityEngine.Random.value;
        if (spawnRoll < tripleSpawnChance)
        {
            spawns = 3;
        }
        else if (spawnRoll < doubleSpawnChance)
        {
            spawns = 2;
        }
        else
        {
            spawns = 1;
        }
        if (initialSpawn && spawns > 2) spawns = 2; 
        return spawns;
    }

    void HandleGameStart()
    {
        Spawn(true);
    }

    void HandleNextLevel()
    {
        Spawn();
    }

    void Awake()
    {
        GameManager.OnGameStart += HandleGameStart;
        GameManager.OnNextLevel += HandleNextLevel;
    }

    void OnDestroy()
    {
        GameManager.OnGameStart -= HandleGameStart;
        GameManager.OnNextLevel -= HandleNextLevel;
    }
}
