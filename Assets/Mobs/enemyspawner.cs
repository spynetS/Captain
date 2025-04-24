// enemy spawner
using System.Collections;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField]
    private GameObject greenOrcPrefab; // Prefab for the green orc enemy
    [SerializeField]
    private GameObject greenOrcWarriorPrefab; // Prefab for the green orc warrior enemy
    [SerializeField]
    private GameObject skeletonPrefab; // Prefab for the skeleton enemy
    [SerializeField]
    private float spawnInterval = 5f; // Time between spawns
    [SerializeField]
    private float warriorSpawnChance = 0.5f; // 50% chance to spawn a warrior
    [SerializeField]
    private float skeletonSpawnChance = 0.25f; // 25% chance to spawn a skeleton
    // The remaining 25% will be for spawning a basic green orc

    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnInterval));
    }

    private IEnumerator SpawnEnemy(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            float randomValue = Random.value;
            GameObject enemyToSpawn;

            if (randomValue < warriorSpawnChance)
            {
                enemyToSpawn = greenOrcWarriorPrefab; // Spawn a warrior
            }
            else if (randomValue < warriorSpawnChance + skeletonSpawnChance)
            {
                enemyToSpawn = skeletonPrefab; // Spawn a skeleton
            }
            else
            {
                enemyToSpawn = greenOrcPrefab; // Spawn a basic green orc
            }

            Instantiate(enemyToSpawn, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity); // Spawn the selected enemy at a random position
        }
    }
}