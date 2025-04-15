using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI buffText;
    public Health health;

    private bool hasStrength;
    private bool hasSpeed;
    private bool hasRegen;

    private float strengthTimer = 0f;
    private float speedTimer = 0f;
    private float regenTimer = 0f;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateUI();

        // Buff activation
        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateStrength();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ActivateSpeed();
        if (Input.GetKeyDown(KeyCode.Alpha3)) ActivateRegen();

        // Buff Timers
        if (hasStrength)
        {
            strengthTimer -= Time.deltaTime;
            if (strengthTimer <= 0f) EndStrength();
        }

        if (hasSpeed)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0f) EndSpeed();
        }
    }


    void UpdateUI()
    {
        healthText.text = $"Health: {health.currentHealth}/{health.maxHealth}";

        string buffs = "";
        if (hasStrength) buffs += "Strength (2x DMG) ";
        if (hasSpeed) buffs += "Speed ";
        if (hasRegen) buffs += "Regen ";

        buffText.text = $"Buffs: {buffs}";
    }

    void ActivateStrength()
    {
        hasStrength = true;
        strengthTimer = 5f;
        sprite.color = Color.red; // Red tint
        Debug.Log("Strength Buff Activated!");
    }

    void EndStrength()
    {
        hasStrength = false;
        sprite.color = Color.white;
        Debug.Log("Strength Buff Ended.");
    }

    void ActivateSpeed()
    {
        if (!hasSpeed) GetComponent<PlayerController>().moveSpeed *= 1.5f;
        hasSpeed = true;
        speedTimer = 5f;
        Debug.Log("Speed Buff Activated!");
    }

    void EndSpeed()
    {
        hasSpeed = false;
        GetComponent<PlayerController>().moveSpeed /= 1.5f;
        Debug.Log("Speed Buff Ended.");
    }

    void ActivateRegen()
    {
        if (!hasRegen)
        {
            hasRegen = true;
            regenTimer = 5f;
            InvokeRepeating("RegenHealth", 1f, 1f);
        }
    }

    void RegenHealth()
    {
        if (health.currentHealth < health.maxHealth)
        {
            health.currentHealth++;
            health.SendMessage("UpdateBar");
        }

        regenTimer -= 1f;
        if (regenTimer <= 0f)
        {
            hasRegen = false;
            CancelInvoke("RegenHealth");
            Debug.Log("Regen Buff Ended.");
        }
    }

    public bool HasStrength()
    {
        return hasStrength;
    }
}
