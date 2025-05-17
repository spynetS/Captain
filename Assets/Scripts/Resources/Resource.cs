using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Resource : YSort, ITakeDamage
{
    public List<GameObject> dropItems; // the items that can be droped
    public List<float>      dropChanse;// the chanse that the item (at the smae index) has to be droped

    public int dropAmount;
    public float maxHealth;
    public float health;
    public Animation hitAnimation;

    public Animator animator;

    public Image healthBar;
    public GameObject healthCanvas;

    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        if (health <= 0)
        {
            health = maxHealth;
        }
        if (healthBar)
        {
            healthBar.fillAmount = health / maxHealth;

            healthBar.gameObject.SetActive(true);
        }
        if(healthCanvas)
        {
            healthCanvas.SetActive(false);
        }
    }

    /**
     *  Method to give damage to the Resource untill it dies and drop the drop items
     * */
    public void TakeDamage(float damage){
        if(healthCanvas)
        {
            healthCanvas.SetActive(true);
        }
        
        audioSource.PlayOneShot(clip);
        this.health -= damage;
        if(hitAnimation != null)
            hitAnimation.Play();
        if(this.health <= 0){
            this.Die();
        }
        audioSource.PlayOneShot(clip);
    }
    void Update(){
        if (healthBar)
        {
            float percent = health / maxHealth;
            healthBar.fillAmount = percent;
            healthBar.gameObject.SetActive(true);
        }

    }

    /** Drops the dropItem and destroyes it self */
    private void Die(){
        for(int i = 0; i < dropAmount; i++){
            for(int j = 0; j < dropItems.Count; j ++){

                float value = Random.value;
                Debug.Log(value);
                if(value <= dropChanse[j]){
                    GameObject dropped = Instantiate(dropItems[j], transform.position, transform.rotation);
                    Vector2 randomDirection = Random.insideUnitCircle.normalized/2;
                    dropped.GetComponent<Rigidbody2D>().AddForce(randomDirection);
                    break;
                }

            }
        }
        if(animator){
            animator.SetBool("dead",true);
            Destroy(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }


}
