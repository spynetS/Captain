using UnityEngine;

public class PlayerBuffStatus : MonoBehaviour
{
    private bool hasStrength;
    private bool hasSpeed;
    private bool hasRegen;

    private float strengthTimer;
    private float speedTimer;
    private float regenTimer;

    private float totalRegenAmount;
    private float regenPerTick;

    private PlayerHealth playerHealth;
    private PlayerController playerController;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
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

        if (hasRegen)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0f) EndRegen();
        }
    }

    // Buff Activation Methods 

    public void ActivateStrength(float duration)
    {
        hasStrength = true;
        strengthTimer = duration;
        Debug.Log("Strength buff applied.");
    }

    public void ActivateSpeed(float duration)
    {
        if (!hasSpeed && playerController != null)
            playerController.moveSpeed *= 1.5f;

        hasSpeed = true;
        speedTimer = duration;
        Debug.Log("Speed buff applied.");
    }

    public void ActivateRegen(float duration, float percent)
    {
        if (!hasRegen && playerHealth != null)
        {
            hasRegen = true;
            regenTimer = duration;

            totalRegenAmount = playerHealth.maxHealth * percent;
            regenPerTick = totalRegenAmount / duration; // per second

            InvokeRepeating(nameof(ApplyRegenTick), 1f, 1f);
            Debug.Log($"Regen buff applied: {percent * 100}% over {duration} seconds.");
        }
    }

    private void ApplyRegenTick()
    {
        if (!hasRegen || playerHealth == null) return;

        int healAmount = Mathf.CeilToInt(regenPerTick);
        playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healAmount, playerHealth.maxHealth);
        playerHealth.UpdateBar();
    }

    // End Buffs 

    private void EndStrength()
    {
        hasStrength = false;
        Debug.Log("Strength buff ended.");
    }

    private void EndSpeed()
    {
        hasSpeed = false;
        if (playerController != null)
            playerController.moveSpeed /= 1.5f;

        Debug.Log("Speed buff ended.");
    }

    private void EndRegen()
    {
        hasRegen = false;
        CancelInvoke(nameof(ApplyRegenTick));
        Debug.Log("Regen buff ended.");
    }
}

