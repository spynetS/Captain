using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownDifficultyArt : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public Sprite easySprite;
    public Sprite normalSprite;
    public Sprite hardSprite;

    public Image backgroundImage; // this is the dropdown's background Image

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDifficultyChanged);
        OnDifficultyChanged(dropdown.value); // set initial
    }

    void OnDifficultyChanged(int index)
    {
        switch (index)
        {
            case 0:
                backgroundImage.sprite = easySprite;
                break;
            case 1:
                backgroundImage.sprite = normalSprite;
                break;
            case 2:
                backgroundImage.sprite = hardSprite;
                break;
        }
    }
}
