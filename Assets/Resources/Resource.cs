using UnityEngine;

public class Resource : MonoBehaviour
{
    public GameObject dropItem;
    public int dropAmount;
    public float health;

    /**
     *  Method to give damage to the Resource untill it dies and drop the drop items
     *  */
    public void TakeDamage(float damage){

        if(this.health-- <= 0){
            this.Die();
        }
    }
    /** Drops the dropItem and destroyes it self */
    private void Die(){

    }
}
