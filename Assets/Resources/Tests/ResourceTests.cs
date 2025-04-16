using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;

public class ResourceTests
{
    private GameObject resourceObj;
    private Resource resource;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject with Resource attached
        resourceObj = new GameObject();
        resource = resourceObj.AddComponent<Resource>();

        // Setup default values
        resource.health = 100;
        resource.dropAmount = 1;
        resource.dropItems = new List<GameObject>();
        resource.dropChanse = new List<float>();

        // Add a dummy Animation
        resource.hitAnimation = resourceObj.AddComponent<Animation>();

        // Add TMP_Text mock
        GameObject tmpTextObj = new GameObject();
        tmpTextObj.transform.parent = resourceObj.transform;
        TMP_Text text = tmpTextObj.AddComponent<TextMeshProUGUI>();
        resource.health_text = text;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(resourceObj);
    }

    [Test]
    public void TakeDamage_ReducesHealth()
    {
        float initialHealth = resource.health;
        resource.TakeDamage(25f);
        Assert.Less(resource.health, initialHealth);
    }

    [UnityTest]
    public IEnumerator TakeDamage_WhenHealthZeroOrLess_CallsDie()
    {
        // Add a dummy drop item with Rigidbody2D
        GameObject dropItem = new GameObject();
        dropItem.AddComponent<Rigidbody2D>();
        resource.dropItems.Add(dropItem);
        resource.dropChanse.Add(1f); // Ensure it drops

        resource.health = 10f;
        resource.TakeDamage(15f); // This should trigger Die()

        // Wait one frame for Destroy to take effect
        yield return null;

        Assert.IsTrue(resource == null || resource.Equals(null)); // Check if destroyed
    }

    [Test]
    public void Update_ChangesHealthText()
    {
        resource.health = 42f;
        resource.Update();
        Assert.AreEqual("42", resource.health_text.text);
    }
}
