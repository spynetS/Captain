using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MobHealth
{
    private GameObject mobObj;
    private Resource mob;

    [SetUp]
    public void Setup()
    {
        mobObj = new GameObject("TestMob");
        mob = mobObj.AddComponent<Resource>();

        mob.health = 20f;
        mob.dropAmount = 1;
        mob.dropItems = new System.Collections.Generic.List<GameObject> { new GameObject("LootDrop") };
        mob.dropChanse = new System.Collections.Generic.List<float> { 1f };

        mob.dropItems[0].AddComponent<Rigidbody2D>();
        mob.animator = mobObj.AddComponent<Animator>();
        mob.hitAnimation = mobObj.AddComponent<Animation>();
    }

    [UnityTest]
    public IEnumerator MobDies_WhenTakingLethalDamage()
    {
        mob.TakeDamage(20f);

        yield return new WaitForSeconds(1.1f);

        Assert.IsTrue(mob == null || mob.Equals(null), "Mob should be destroyed after taking lethal damage.");
    }

    [UnityTest]
    public IEnumerator LootDrops_WhenMobDies()
    {
        int lootBefore = GameObject.FindObjectsOfType<Rigidbody2D>().Length;

        mob.TakeDamage(100f);
        yield return new WaitForSeconds(0.1f);

        int lootAfter = GameObject.FindObjectsOfType<Rigidbody2D>().Length;

        Assert.Greater(lootAfter, lootBefore, "Loot should be dropped on mob death.");
    }

    [TearDown]
    public void Teardown()
    {
        if (mobObj)
        {
            if (Application.isPlaying)
                Object.Destroy(mobObj);
            else
                Object.DestroyImmediate(mobObj);
        }

        foreach (var item in GameObject.FindObjectsOfType<GameObject>())
        {
            if (item.name.Contains("LootDrop"))
            {
                if (Application.isPlaying)
                    Object.Destroy(item);
                else
                    Object.DestroyImmediate(item);
            }
        }
    }
}
