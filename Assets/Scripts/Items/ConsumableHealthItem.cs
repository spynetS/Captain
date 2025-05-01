using UnityEngine;

/**
 * This is a item that the player can consume, either eating, drink
 * etc. When used it
 *  */
public class ConsumableHealthItem : Item
{
    public int healthGain;

    public override void Use(InventorySystem inventory)
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        // if the health is lower then max then we consume
        if(health.currentHealth < health.maxHealth){

            health.currentHealth += healthGain;
            health.UpdateBar();
            inventory.DestroyItem(this);
        }
    }
}
