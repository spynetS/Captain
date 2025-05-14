using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Alfred"); // Replace with your actual scene name
    }

    public void ShowInstructions()
    {
        // Display a panel or load instructions scene
        Debug.Log("Instructions button clicked!");
    }

    public void OpenSettings()
    {
        // Display a settings panel
        Debug.Log("Settings button clicked!");
    }

    public class QuitHandler : MonoBehaviour
{
    public static bool quitCalled = false;

    public void QuitGame()
    {
    #if UNITY_EDITOR
        quitCalled = true;
        Debug.Log("Game is exiting.");
    #else
        Application.Quit();
    #endif
    }
    }

}
