// enemy

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject PlayerObject { get; set; }
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private EnemyData data;

    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        //this.SetEnemyValues(); //set the values of the enemy
    }

    void Update()
    {
        Swarm();
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp); //set health of the enemy to the value in the scriptable object
        damage = data.damage; 
        speed = data.speed;
    }

    private void Swarm()
    {
        if(playerObject)
            transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime); //move towards the player
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent("Player"))
        {
            if(collider.GetComponent<Health>() != null) //if collider has health component
            {
                collider.GetComponent<Health>().TakeDamage(damage); //damage the player
                this.GetComponent<Health>().TakeDamage(10000); //damage the enemy
            }
        }
    }
}