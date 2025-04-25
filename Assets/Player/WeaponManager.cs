using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject sword;
    public TextMeshProUGUI weaponDisplay;

    private enum WeaponType { Gun, Sword }
    private WeaponType currentWeapon = WeaponType.Gun;

    void Start()
    {
        SetWeapon(currentWeapon); // Start with gun
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentWeapon = WeaponType.Gun;
            SetWeapon(currentWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentWeapon = WeaponType.Sword;
            SetWeapon(currentWeapon);
        }
    }

    void SetWeapon(WeaponType weapon)
    {
        if (weapon == WeaponType.Gun)
        {
            gun.SetActive(true);
            sword.SetActive(false);
            weaponDisplay.text = "Weapon: Gun";
        }
        else if (weapon == WeaponType.Sword)
        {
            gun.SetActive(false);
            sword.SetActive(true);
            weaponDisplay.text = "Weapon: Sword";
        }
    }

    public bool IsUsingGun()
    {
        return currentWeapon == WeaponType.Gun;
    }

    public bool IsUsingSword()
    {
        return currentWeapon == WeaponType.Sword;
    }
}
