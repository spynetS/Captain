using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField] private GameObject greenOrcPrefab;
    [SerializeField] private GameObject greenOrcWarriorPrefab;
    [SerializeField] private GameObject skeletonPrefab;

    [SerializeField] public float spawnInterval = 1f; // Can be changed at runtime
    [SerializeField] public float warriorSpawnChance = 0.1f;
    [SerializeField] public float skeletonSpawnChance = 0.05f;

    public static bool spawning = false;

    private float spawnTimer = 0f;

    void Update()
    {
        if (!spawning) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // reset the timer
        }
    }

    private void SpawnEnemy()
    {
        float randomValue = Random.value;
        GameObject enemyToSpawn;

        if (randomValue < warriorSpawnChance)
        {
            enemyToSpawn = greenOrcWarriorPrefab;
        }
        else if (randomValue < warriorSpawnChance + skeletonSpawnChance)
        {
            enemyToSpawn = skeletonPrefab;
        }
        else
        {
            enemyToSpawn = greenOrcPrefab;
        }

        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0) + transform.position;
        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
    }
}
