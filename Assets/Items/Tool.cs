using UnityEngine;

/**
 * This is a item that the player can consume, either eating, drink
 * etc. When used it
 *  */
public class ToolItem : Item
{
    public float damage;
    public CircleCollider2D collider;
    public override void Use()
    {
        this.collider.enabled = true;
    }
}
