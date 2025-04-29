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
    public float stopDistance = 1f; // or whatever distance you want

    private GameObject playerObject;

    private float timer = 0;
    private float maxTimer = 10;
    public Collider2D attackSpace;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");



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

     public void FixedUpdate(){
        if(timer >= maxTimer){
            if(attackSpace.enabled) attackSpace.enabled = false;
            timer = 0;
        }
        if(attackSpace.enabled) timer ++;

    }


    private void Swarm()
    {
        if (playerObject)
        {
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            if (distance > stopDistance) // stopDistance is the distance you want to keep
            {
                transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
            }
            else
            {
                attackSpace.enabled = true;
            }
        }
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
