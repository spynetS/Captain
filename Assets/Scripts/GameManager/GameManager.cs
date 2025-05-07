using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentCycle = 1;
    public float dayDuration = 180f;
    public float nightDuration = 120f;

    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dayCounterText;
    public Image screenTint;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    private float currentTime = 0f;
    private bool isDay = true;
    private bool gameHasEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        StartDay();
    }

    void Update()
    {
        if (gameHasEnded) return;

        currentTime += Time.deltaTime;
        float duration = isDay ? dayDuration : nightDuration;

        UpdateClock(currentTime / duration);

        if (currentTime >= duration)
        {
            currentTime = 0f;
            isDay = !isDay;

            currentCycle++;

            if (isDay)
                StartDay();
            else
                StartNight();
        }
    }

    void StartDay()
    {
        screenTint.color = new Color(0, 0, 0, 0);
        int dayNumber = Mathf.CeilToInt(currentCycle / 2f);
        dayCounterText.text = $"Day {dayNumber}";

        Debug.Log("Day Started! Cycle: " + currentCycle);
    }

    void StartNight()
    {
        int nightNumber = Mathf.CeilToInt(currentCycle / 2f);

        if (nightNumber == 10)
        {
            screenTint.color = new Color(0.7f, 0f, 0f, 0.75f);
            dayCounterText.text = "Last Night";

            // Win the game after night ends
            Invoke(nameof(WinGame), nightDuration);
        }
        else if (nightNumber == 5)
        {
            screenTint.color = new Color(0.6f, 0f, 0f, 0.7f);
            dayCounterText.text = "Crimson Night";
        }
        else
        {
            screenTint.color = new Color(0.05f, 0.05f, 0.2f, 0.7f);
            dayCounterText.text = $"Night {nightNumber}";
        }

        Debug.Log("Night Started! Cycle: " + currentCycle);
    }

    void UpdateClock(float t)
    {
        int hour;
        if (isDay)
        {
            hour = Mathf.FloorToInt(Mathf.Lerp(7f, 21f, t));
        }
        else
        {
            hour = Mathf.FloorToInt(Mathf.Lerp(21f, 31f, t));
            if (hour >= 24) hour -= 24;
        }

        string suffix = hour >= 12 ? "PM" : "AM";
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12;

        clockText.text = $"{displayHour}:00 {suffix}";
    }

    // Called when player wins
    public void WinGame()
    {
        if (gameHasEnded) return;

        gameHasEnded = true;
        Time.timeScale = 0f;
        victoryScreen.SetActive(true);
        Debug.Log("You Win!");
    }

    // Called when base is destroyed
    public void BaseDestroyed()
    {
        if (gameHasEnded) return;

        gameHasEnded = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        Debug.Log("Game Over! Base destroyed.");
    }

    // Restart button calls this
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

