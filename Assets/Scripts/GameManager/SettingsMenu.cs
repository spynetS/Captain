using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public TMP_Dropdown difficultyDropdown;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        string savedDifficulty = PlayerPrefs.GetString("GameDifficulty", "Normal");
        int index = difficultyDropdown.options.FindIndex(option => option.text == savedDifficulty);
        if (index >= 0) difficultyDropdown.value = index;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null) music.volume = value;
    }

    public void SetDifficulty(int index)
    {
        string difficulty = difficultyDropdown.options[index].text;
        PlayerPrefs.SetString("GameDifficulty", difficulty);
        PlayerPrefs.Save(); //  Force save immediately
        Debug.Log("Difficulty saved: " + difficulty);
    }

    public void StartGame()
    {
        // 🔁 Optional button method to start game AFTER difficulty is set
        PlayerPrefs.Save(); // extra safety
        SceneManager.LoadScene("YourGameScene"); // replace with actual scene name
    }
}
