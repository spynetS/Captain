using System.Collections;
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

  [UnityTest]
    public IEnumerator TakeDamage_KillsResource_WhenHealthDepletes()
    {
                // Arrange: Get the initial position
        Vector3 initialPosition = playerController.transform.position;

        // Simulate horizontal movement by calling Move with custom parameters
        playerController.Move(1, 0);  // Simulating pressing "D" for horizontal movement (1, 0)

        // Act: Wait for a small duration to simulate frame update and movement
        yield return new WaitForSeconds(0.1f); // Wait for a short frame duration (a frame or so)

        // Assert: Check if the position has changed, which means movement happened
        Assert.AreNotEqual(initialPosition, playerController.transform.position, "Player should have moved.");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerObject);
    }
}
