using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ResourceTests
{
    private GameObject resourceObj;
    private Resource resource;

    [SetUp]
    public void Setup()
    {
        resourceObj = new GameObject("TestResource");
        resource = resourceObj.AddComponent<Resource>();

        // Set up default values
        resource.health = 10f;
        resource.dropAmount = 1;
        resource.dropItems = new System.Collections.Generic.List<GameObject> { new GameObject("DropItem") };
        resource.dropChanse = new System.Collections.Generic.List<float> { 1f }; // guaranteed drop

        var rb = resource.dropItems[0].AddComponent<Rigidbody2D>();
        resource.animator = resourceObj.AddComponent<Animator>();
        resource.hitAnimation = resourceObj.AddComponent<Animation>();
    }

    [UnityTest]
    public IEnumerator TakeDamage_KillsResource_WhenHealthDepletes()
    {
        // Act
        resource.TakeDamage(10f);

        // Wait a frame to allow destruction
        yield return new WaitForSeconds(1.1f);

        // Assert
        Assert.IsTrue(resource == null || resource.Equals(null)); // Unity overloads == for destroyed objects
        yield return null; // after assertions
    }

    [UnityTest]
    public IEnumerator DropItems_SpawnsOnDeath()
    {
        int initialCount = GameObject.FindObjectsOfType<Rigidbody2D>().Length;

        resource.TakeDamage(100f);
        yield return null; // Let frame pass

        int afterCount = GameObject.FindObjectsOfType<Rigidbody2D>().Length;
        Assert.Greater(afterCount, initialCount);
        yield return null; // after assertions
    }

    [TearDown]
    public void Teardown()
    {
        if (resourceObj)
        {
            if (Application.isPlaying)
                Object.Destroy(resourceObj);
            else
                Object.DestroyImmediate(resourceObj);
        }

        foreach (var item in GameObject.FindObjectsOfType<GameObject>())
        {
            if (item.name.Contains("DropItem"))
            {
                if (Application.isPlaying)
                    Object.Destroy(item);
                else
                    Object.DestroyImmediate(item);
            }
        }
    }

}
