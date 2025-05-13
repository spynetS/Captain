using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class EnemyMovement
{
    private GameObject enemyObject;
    private Enemy enemyScript;

    [SetUp]
    public void SetUp()
    {
        // Set up the enemy GameObject and add required components
        enemyObject = new GameObject("Enemy");
        enemyScript = enemyObject.AddComponent<Enemy>();
        enemyObject.AddComponent<Rigidbody2D>();
        enemyScript.swingAnimator = enemyObject.AddComponent<Animator>();

        // Optionally, set up other required objects (e.g., Player object)
        enemyScript.PlayerObject = new GameObject("Player");
        enemyScript.PlayerObject.transform.position = new Vector3(10, 0, 0);  // Simulate the player position
    }

    [UnityTest]
    public IEnumerator TestEnemyMovementTowardsPlayer()
    {
        // Save the initial position of the enemy
        float initialPositionX = enemyObject.transform.position.x;

        // Set enemy detection range and speed directly (public)
        enemyScript.detectionRange = 15f;  // Player is within detection range
        enemyScript.speed = 2f;  // Set speed directly (public field)

        // Call Swarm to make the enemy move towards the player
        enemyScript.Swarm();

        // Wait for a frame to allow the movement to happen
        yield return null;

        // Assert that the enemy has moved towards the player
        float newPositionX = enemyObject.transform.position.x;
        Assert.Greater(newPositionX, initialPositionX, "Enemy should move towards the player.");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the enemy object after the test
        Object.DestroyImmediate(enemyObject);
    }
}
