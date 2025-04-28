using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject victoryPanel;

    private bool gameEnded = false;

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

