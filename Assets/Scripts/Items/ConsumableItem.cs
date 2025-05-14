using UnityEngine;

/**
 * A consumable item that can heal and/or apply buffs to player.
 */

public class ConsumableItem : Item
{
    [Header("Healing")]
    [SerializeField] private int healthGain = 0;

    [Header("Buff Options")]
    [SerializeField] private bool grantsStrength = false;
    [SerializeField] private float strengthDuration = 5f;

    [SerializeField] private bool grantsSpeed = false;
    [SerializeField] private float speedDuration = 5f;

    [SerializeField] private bool grantsRegen = false;
    [SerializeField] private float regenDuration = 5f;

    [Tooltip("Percent of max health to regen over the buff duration (e.g., 0.1 = 10%)")]
    [SerializeField] private float regenPercent = 0.1f;

    public override void Use(InventorySystem inventory)
    {

        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        bool used = false;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        PlayerBuffStatus buffStatus = player.GetComponent<PlayerBuffStatus>();

        // Heal immediately
        if (healthGain > 0 && playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healthGain, playerHealth.maxHealth);
            playerHealth.UpdateBar();
            used = true;
        }

        // Apply buffs
        if (buffStatus != null)
        {
            if (grantsStrength ) buffStatus.ActivateStrength(strengthDuration); used = true;
            if (grantsSpeed) buffStatus.ActivateSpeed(speedDuration); used = true;
            if (grantsRegen) buffStatus.ActivateRegen(regenDuration, regenPercent); used = true;

        }
        if(used)
            inventory.DestroyItem(this);
    }
}
