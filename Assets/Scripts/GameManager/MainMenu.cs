using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your actual scene name
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

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
