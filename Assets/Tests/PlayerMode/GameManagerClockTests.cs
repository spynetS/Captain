using NUnit.Framework;
using UnityEngine;

public class GameManagerClockLogicTests
{
    private GameManager gm;

    [SetUp]
    public void Setup()
    {
        gm = new GameObject("TestGameManager").AddComponent<GameManager>();
    }

    [Test]
    public void Clock_ReturnsCorrectHour_AtStartOfDay()
    {
        string result = gm.GetClockDisplay(0f, true); // 0% through the day
        Assert.AreEqual("7:00 AM", result);
    }

    

    [Test]
    public void Clock_ReturnsCorrectHour_EndOfDay()
    {
        string result = gm.GetClockDisplay(1f, true); // 100% through the day
        Assert.AreEqual("9:00 PM", result);
    }

    [Test]
    public void Clock_ReturnsCorrectHour_AtStartOfNight()
    {
        string result = gm.GetClockDisplay(0f, false); // 0% through the night
        Assert.AreEqual("9:00 PM", result);
    }

    

    [Test]
    public void Clock_WrapsCorrectly_AtEndOfNight()
    {
        string result = gm.GetClockDisplay(1f, false); // 100% through the night
        Assert.AreEqual("7:00 AM", result);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gm.gameObject);
    }
}

