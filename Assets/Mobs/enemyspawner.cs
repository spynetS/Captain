// enemy spawner
using System.Collections;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField]
    private GameObject redPrefab;
    [SerializeField]
    private GameObject bluePrefab;

    [SerializeField]
    private float redInterval = 3.5f; // Time between spawns
    [SerializeField]
    private float blueInterval = 10f; // Time between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy(redInterval, redPrefab)); // Start the coroutine to spawn red enemies
        StartCoroutine(SpawnEnemy(blueInterval, bluePrefab)); // Start the coroutine to spawn blue enemies
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