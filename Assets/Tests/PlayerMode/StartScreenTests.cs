using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class StartScreenTests
{
    [UnityTest]
    public IEnumerator StartGameButton_SwitchesToAlfredScene()
    {
        SceneManager.LoadScene("GameMenyScreen"); 
        yield return null;

        yield return new WaitForSeconds(1f); // Let UI load

        var startButton = GameObject.Find("StartGameButton")?.GetComponent<Button>();
        Assert.IsNotNull(startButton, "StartGameButton not found!");

        startButton.onClick.Invoke(); // Simulate click
        yield return new WaitForSeconds(1f); // Wait for scene to switch

        Assert.AreEqual("Alfred", SceneManager.GetActiveScene().name);
    }
}
