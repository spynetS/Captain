using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerControllerTests
{
    private GameObject playerObject;
    private PlayerController playerController;
    private InventorySystem inventory;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject("Player");
        playerController = playerObject.AddComponent<PlayerController>();
        inventory = playerObject.AddComponent<InventorySystem>();
        playerController.inventory = inventory;
        playerController.moveSpeed = 5f;
    }
    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerObject);
    }
}
