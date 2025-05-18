using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel; // Assign in Inspector

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your actual scene name
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked!");
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting.");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
