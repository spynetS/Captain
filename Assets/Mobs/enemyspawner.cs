// enemy spawner
using System.Collections;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField]
    private GameObject greenOrcPrefab; // Prefab for the green orc enemy
    [SerializeField]
    private float spawnInterval = 5f; // Time between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnInterval, greenOrcPrefab)); // Start the coroutine to spawn GreenOrc enemies
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        while (true) // Infinite loop to keep spawning enemies
        {
            yield return new WaitForSeconds(interval);
            Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity); // Spawn enemy at a random position
        }
    }
}