using UnityEngine;
using System.Collections.Generic;

public class FenceLocation : MonoBehaviour
{
    public GameObject[] pillarPrefabs;
    public GameObject[] wallVerticalPrefabs;
    public GameObject[] wallHorizontalPrefabs;
    public List<GameObject> placedPillars = new List<GameObject>();
    public List<GameObject> placedVerticalWalls = new List<GameObject>();
    public List<GameObject> placedWallsHorizontal = new List<GameObject>();

    private bool wallsPlaced = false;
    private int currentPillarPrefabIndex = 0;
    private int currentWallVerticalPrefabIndex = 0;
    private int currentWallHorizontalPrefabIndex = 0;

    public int AmountToRepair(){
        int amount = 0;
        foreach(GameObject ob in placedPillars){
            if(ob == null) amount++;
        }
        foreach(GameObject ob in placedWallsHorizontal){
            if(ob == null) amount++;
        }
        foreach(GameObject ob in placedWallsHorizontal){
            if(ob == null) amount++;
        }

        return amount;
    }

    private Vector3[] wallVerticalPositions = new Vector3[]
    {
        // left side
        new Vector3(-3, 2.5f, 0),
        new Vector3(-3, 1.5f, 0),
        new Vector3(-3, 0.5f, 0),
        new Vector3(-3, -0.5f, 0),
        new Vector3(-3, -1.5f, 0),

        // right side
        new Vector3(3, 2.5f, 0),
        new Vector3(3, 1.5f, 0),
        new Vector3(3, 0.5f, 0),
        new Vector3(3, -0.5f, 0),
        new Vector3(3, -1.5f, 0),
    };

    private Vector3[] pillarPositions = new Vector3[]
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

        // left side
        new Vector3(-3, 2, 0),
        new Vector3(-3, 1, 0),
        new Vector3(-3, 0, 0),
        new Vector3(-3, -1, 0),
        new Vector3(-3, -2, 0),

        // right side
        new Vector3(3, 2, 0),
        new Vector3(3, 1, 0),
        new Vector3(3, 0, 0),
        new Vector3(3, -1, 0),
        new Vector3(3, -2, 0)
    };

    private Vector3[] wallHorizontolPositions = new Vector3[]
    {
        // down side
        new Vector3(-2.5f, -2, 0),
        new Vector3(-1.5f, -2, 0),
        new Vector3(-0.5f, -2, 0),
        new Vector3(0.5f, -2, 0),
        new Vector3(1.5f, -2, 0),
        new Vector3(2.5f, -2, 0),

        // up side
        new Vector3(-2.5f, 3, 0),
        new Vector3(-1.5f, 3, 0),
        new Vector3(-0.5f, 3, 0),
        new Vector3(0.5f, 3, 0),
        new Vector3(1.5f, 3, 0),
        new Vector3(2.5f, 3, 0),
    };

    public void PlaceWalls(){
        PlaceraPillars();
        PlaceraVerticalVäggar();
        PlaceraHorizontalVäggar();
        wallsPlaced = true;
    }

    public void UpgradeWalls(int amount){
        BytPillarPrefab(amount); // Byt till prefab på index amount
        BytWallVerticalPrefab(amount); // Byt till prefab på index amount
        BytWallHorizontalPrefab(amount); // Byt till prefab på index 1
    }

    void PlaceraPillars()
    {
        foreach (Vector3 position in pillarPositions)
        {
            Vector3 adjustedPosition = new Vector3(position.x, position.y, -1)+this.transform.position;
            GameObject pillar = Instantiate(pillarPrefabs[currentPillarPrefabIndex], adjustedPosition, Quaternion.identity);
            placedPillars.Add(pillar);
        }
    }

    void PlaceraVerticalVäggar()
    {
        foreach (Vector3 position in wallVerticalPositions)
        {
            Vector3 adjustedPosition = new Vector3(position.x, position.y, 0)+this.transform.position;
            GameObject wall = Instantiate(wallVerticalPrefabs[currentWallVerticalPrefabIndex], adjustedPosition, Quaternion.identity);
            placedVerticalWalls.Add(wall);
        }
    }

    void PlaceraHorizontalVäggar()
    {
        foreach (Vector3 position in wallHorizontolPositions)
        {
            GameObject wall = Instantiate(wallHorizontalPrefabs[currentWallHorizontalPrefabIndex], position+this.transform.position, Quaternion.identity);
            placedWallsHorizontal.Add(wall);
        }
    }

    void BytPillarPrefab(int newIndex)
    {
        if (newIndex < 0 || newIndex >= pillarPrefabs.Length) return;

        foreach (GameObject wall in placedPillars)
        {
            Destroy(wall);
        }

        placedPillars.Clear();
        currentPillarPrefabIndex = newIndex;
        PlaceraPillars();
    }

    void BytWallVerticalPrefab(int newIndex)
    {
        if (newIndex < 0 || newIndex >= wallVerticalPrefabs.Length) return;

        foreach (GameObject wall in placedVerticalWalls)
        {
            Destroy(wall);
        }

        placedVerticalWalls.Clear();
        currentWallVerticalPrefabIndex = newIndex;
        PlaceraVerticalVäggar();
    }

    void BytWallHorizontalPrefab(int newIndex)
    {
        if (newIndex < 0 || newIndex >= wallHorizontalPrefabs.Length) return;

        foreach (GameObject wall in placedWallsHorizontal)
        {
            Destroy(wall);
        }

        placedWallsHorizontal.Clear();
        currentWallHorizontalPrefabIndex = newIndex;
        PlaceraHorizontalVäggar();
    }
}
