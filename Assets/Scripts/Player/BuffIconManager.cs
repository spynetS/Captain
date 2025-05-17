using UnityEngine;
using UnityEngine.UI;

public class BuffIconManager : MonoBehaviour
{
    public Image strengthIcon;
    public Image speedIcon;
    public Image regenIcon;

    private void Start()
    {
        strengthIcon?.gameObject.SetActive(false);
        speedIcon?.gameObject.SetActive(false);
        regenIcon?.gameObject.SetActive(false);
    }

    public void ShowStrengthIcon() => strengthIcon?.gameObject.SetActive(true);
    public void HideStrengthIcon() => strengthIcon?.gameObject.SetActive(false);

    public void ShowSpeedIcon() => speedIcon?.gameObject.SetActive(true);
    public void HideSpeedIcon() => speedIcon?.gameObject.SetActive(false);

    public void ShowRegenIcon() => regenIcon?.gameObject.SetActive(true);
    public void HideRegenIcon() => regenIcon?.gameObject.SetActive(false);
}
