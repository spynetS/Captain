using UnityEngine;

public class FoliageAreaSpawner : MonoBehaviour
{
    [Header("Foliage Settings")]
    public GameObject[] foliagePrefabs;
    public float spawnChance = 0.3f;

    [Header("Spawn Area")]
    public Vector2 areaSize = new Vector2(10, 10);
    public int density = 100; // Number of spawn attempts

    [ContextMenu("Spawn Foliage")]
    public void SpawnFoliage()
    {
        if (foliagePrefabs == null || foliagePrefabs.Length == 0)
        {
            Debug.LogWarning("No foliage prefabs assigned.");
            return;
        }

        for (int i = 0; i < density; i++)
        {
            if (Random.value <= spawnChance)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                    Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                    0f
                );

                Vector3 worldPos = transform.position + randomPos;

                GameObject prefab = foliagePrefabs[Random.Range(0, foliagePrefabs.Length)];
                Instantiate(prefab, worldPos, Quaternion.identity, this.transform);
            }
        }
    }

    [ContextMenu("Clear Foliage")]
    public void ClearFoliage()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0.1f));
    }
}
