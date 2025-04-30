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
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Swarm();
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private void Swarm()
    {
        if (playerObject)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, playerObject.transform.position, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent("Player"))
        {
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().TakeDamage(damage);
                this.GetComponent<Health>().TakeDamage(10000);
            }
        }
    }
}
