using UnityEngine;
using System.Collections.Generic;

public class FenceLocation : MonoBehaviour
{
    public GameObject[] wallPrefabs;
    private List<GameObject> placedWalls = new List<GameObject>();

    private bool wallsPlaced = false;
    private int currentPrefabIndex = 0;
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(-3, -2, 0),
        new Vector3(-2, -2, 0),
        new Vector3(-1, -2, 0),
        new Vector3(0, -2, 0),
        new Vector3(1, -2, 0),
        new Vector3(2, -2, 0),
        new Vector3(3, -2, 0),

        new Vector3(-3, 3, 0),
        new Vector3(-2, 3, 0),
        new Vector3(-1, 3, 0),
        new Vector3(0, 3, 0),
        new Vector3(1, 3, 0),
        new Vector3(2, 3, 0),
        new Vector3(3, 3, 0),

        new Vector3(-3, 2, 0),
        new Vector3(-3, 1, 0),
        new Vector3(-3, 0, 0),
        new Vector3(-3, -1, 0),
        new Vector3(-3, -2, 0),

        new Vector3(3, 2, 0),
        new Vector3(3, 1, 0),
        new Vector3(3, 0, 0),
        new Vector3(3, -1, 0),
        new Vector3(3, -2, 0)
    };
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !wallsPlaced)
        {
            PlaceraVäggar();
            wallsPlaced = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            BytPrefab(1); // Byt till prefab på index 1
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            BytPrefab(2); // Byt till prefab på index 2
        }
    }

    void PlaceraVäggar()
    {
        foreach (Vector3 position in positions)
        {
            GameObject wall = Instantiate(wallPrefabs[currentPrefabIndex], position, Quaternion.identity);
            placedWalls.Add(wall);
        }
    }

    void BytPrefab(int newIndex)
    {
        if (newIndex < 0 || newIndex >= wallPrefabs.Length) return;

        foreach (GameObject wall in placedWalls)
        {
            Destroy(wall);
        }

        placedWalls.Clear();
        currentPrefabIndex = newIndex;
        PlaceraVäggar();
    }
}
