using UnityEngine;
using TMPro;


public class Resource : MonoBehaviour
{
    public GameObject dropItem;
    public int dropAmount;
    public float health;

    public TMP_Text health_text;

    /**
     *  Method to give damage to the Resource untill it dies and drop the drop items
     *  */
    public void TakeDamage(float damage){

        if(this.health-- <= 0){
            this.Die();
        }
    }
    void Update(){
        health_text.text = health.ToString();
    }

    /** Drops the dropItem and destroyes it self */
    private void Die(){

    }


    private void OnTriggerEnter(Collider other)
    {
        health-=10;
    }


}
