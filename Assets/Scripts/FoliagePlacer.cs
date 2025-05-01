using UnityEngine;
using UnityEngine.Tilemaps;

public class FoliageTilemapPlacer : MonoBehaviour
{
    [Header("Foliage Settings")]
    public GameObject[] foliagePrefabs;
    public float spawnChance = 0.3f;

    [Header("References")]
    public Tilemap targetTilemap; // The tilemap to read from (like Ground)
    public Vector2 offset = Vector2.zero; // Optional: adjust spawn height

    [ContextMenu("Place Foliage")]
    public void PlaceFoliage()
    {
        if (foliagePrefabs == null || foliagePrefabs.Length == 0 || targetTilemap == null)
        {
            Debug.LogWarning("Missing foliage prefabs or tilemap reference!");
            return;
        }

        // Get bounds of the tilemap
        BoundsInt bounds = targetTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = targetTilemap.GetTile(cellPosition);

                if (tile != null && Random.value <= spawnChance)
                {
                    Vector3 worldPos = targetTilemap.CellToWorld(cellPosition) + (Vector3)offset;

                    GameObject prefab = foliagePrefabs[Random.Range(0, foliagePrefabs.Length)];
                    Instantiate(prefab, worldPos, Quaternion.identity, this.transform);
                }
            }
        }
    }

    [ContextMenu("Clear Foliage")]
    public void ClearFoliage()
    {
        // Delete all placed foliage
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
