using UnityEngine;

/**
 * This is a item that the player can consume, either eating, drink
 * etc. When used it
 *  */
public class Tool : Item , IGiveDamage
{
    public float damage;
    public Collider2D collider;
    private float timer = 0;
    private float maxTimer = 10;

    public void Start(){
        this.collider.enabled = false;
    }

    public float GiveDamage(){
        return damage;
    }

    public void FixedUpdate(){
        if(timer >= maxTimer){
            if(collider.enabled) collider.enabled = false;
            timer = 0;
        }
        if(collider.enabled) timer ++;

    }

    public void Use(){
        collider.enabled = true;
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Use();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        Resource giveDamage = other.GetComponent<Resource>();
        if(giveDamage != null){
            giveDamage.TakeDamage(GiveDamage());
        }
    }

}
