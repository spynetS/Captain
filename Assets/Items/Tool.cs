using UnityEngine;

/**
 * This is a item that the player can consume, either eating, drink
 * etc. When used it
 *  */
public class ToolItem : Item
{
    public float damage;
    public Collider2D collider;
    public override void Use()
    {
        this.collider.enabled = true;

    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Use();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Resource r = other.GetComponent<Resource>();
        if(r)
         {
            r.health -= damage;
            this.collider.enabled = false;
        }
    }
}
