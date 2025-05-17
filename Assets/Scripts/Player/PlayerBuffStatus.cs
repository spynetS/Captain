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
    private BuffIconManager buffIconManager;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        buffIconManager = FindObjectOfType<BuffIconManager>();
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

    public void ActivateStrength(float duration)
    {
        hasStrength = true;
        strengthTimer = duration;
        buffIconManager?.ShowStrengthIcon();
        Debug.Log("Strength buff applied.");
    }

    public void ActivateSpeed(float duration)
    {
        if (!hasSpeed && playerController != null)
            playerController.moveSpeed *= 1.5f;

        hasSpeed = true;
        speedTimer = duration;
        buffIconManager?.ShowSpeedIcon();
        Debug.Log("Speed buff applied.");
    }

    public void ActivateRegen(float duration, float percent)
    {
        if (!hasRegen && playerHealth != null)
        {
            hasRegen = true;
            regenTimer = duration;
            totalRegenAmount = playerHealth.maxHealth * percent;
            regenPerTick = totalRegenAmount / duration;

            InvokeRepeating(nameof(ApplyRegenTick), 1f, 1f);
            buffIconManager?.ShowRegenIcon();
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

    private void EndStrength()
    {
        hasStrength = false;
        buffIconManager?.HideStrengthIcon();
        Debug.Log("Strength buff ended.");
    }

    private void EndSpeed()
    {
        hasSpeed = false;
        if (playerController != null)
            playerController.moveSpeed /= 1.5f;

        buffIconManager?.HideSpeedIcon();
        Debug.Log("Speed buff ended.");
    }

    private void EndRegen()
    {
        hasRegen = false;
        CancelInvoke(nameof(ApplyRegenTick));
        buffIconManager?.HideRegenIcon();
        Debug.Log("Regen buff ended.");
    }
}

