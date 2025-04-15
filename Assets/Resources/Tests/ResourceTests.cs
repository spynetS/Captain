using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    // [Test]
    // public void NewTestScriptSimplePasses()
    // {
    //     Assert.AreEqual(true,true);
    // }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        Assert.AreEqual(true,true);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
