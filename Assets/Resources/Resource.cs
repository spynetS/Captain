using UnityEngine;
using TMPro;
using System.Collections;

public class Resource : MonoBehaviour
{
    public GameObject dropItem;
    public int dropAmount;
    public float health;
    public Animation hitAnimation;

    public Animator animator;


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
            GameObject dropped = Instantiate(dropItem, transform.position, transform.rotation);
            Vector2 randomDirection = Random.insideUnitCircle.normalized/2;
            dropped.GetComponent<Rigidbody2D>().AddForce(randomDirection);
        }
        animator.SetBool("dead",true);
        Destroy(this.gameObject,1);
    }


}
