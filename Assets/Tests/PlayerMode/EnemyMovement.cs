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
        enemyObject = new GameObject("Enemy");
        enemyScript = enemyObject.AddComponent<Enemy>();
        enemyObject.AddComponent<Rigidbody2D>();
        enemyScript.swingAnimator = enemyObject.AddComponent<Animator>();

        enemyScript.PlayerObject = new GameObject("Player");
        enemyScript.PlayerObject.transform.position = new Vector3(10, 0, 0);  // Simulate the player position
    }

    [UnityTest]
    public IEnumerator TestEnemyMovementTowardsPlayer()
    {
        float initialPositionX = enemyObject.transform.position.x;

        GameObject playerObj = new GameObject("Player");
        playerObj.tag = "Player";
        playerObj.transform.position = new Vector3(10, 0, 0);

        GameObject baseObj = new GameObject("Base");
        baseObj.tag = "Base";
        baseObj.transform.position = new Vector3(-10, 0, 0);

        enemyScript.PlayerObject = playerObj;

        enemyScript.GetType().GetField("playerObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(enemyScript, playerObj);

        enemyScript.GetType().GetField("baseObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .SetValue(enemyScript, baseObj);

        enemyScript.detectionRange = 15f;
        enemyScript.speed = 2f;

        enemyScript.Swarm();

        yield return new WaitForSeconds(0.1f);

        float newPositionX = enemyObject.transform.position.x;
        Assert.Greater(newPositionX, initialPositionX, "Enemy should move towards the player.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemyObject);
    }
}
