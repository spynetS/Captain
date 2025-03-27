using UnityEngine;
using TMPro;


public class Resource : MonoBehaviour
{
    public GameObject dropItem;
    public int dropAmount;
    public float health;
    public Animation hitAnimation;

    public TMP_Text health_text;

    /**
     *  Method to give damage to the Resource untill it dies and drop the drop items
     *  */
    public void TakeDamage(float damage){
        this.health -= damage;
        hitAnimation.Play();
        if(this.health <= 0){
            this.Die();
        }
    }
    void Update(){
        health_text.text = health.ToString();
    }

    /** Drops the dropItem and destroyes it self */
    private void Die(){
        for(int i = 0; i < dropAmount; i++){
            Vector3 pos = new Vector3(i,0,0);
            Instantiate(dropItem,transform.position+pos,transform.rotation);
        }
        Destroy(this.gameObject);
    }


//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        IGiveDamage giveDamage = other.GetComponent<IGiveDamage>();
//        if(giveDamage != null){
//            TakeDamage(giveDamage.GiveDamage());
//        }
//    }
//

}
