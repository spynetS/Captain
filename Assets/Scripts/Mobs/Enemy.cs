using Codice.CM.Common;
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

    public float detectionRange = 5f; // distance at which the enemy detects the player

    public float hitRadius = 0.5f;
    private int defaultLayerMask = 1 << 0;

    private GameObject playerObject;
    private GameObject baseObject;
    private Rigidbody2D rb;

    private float timer = 0;
    private float maxTimer = 10;
    public Animator swingAnimator;

    public int Damage => damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        baseObject = GameObject.FindGameObjectWithTag("Base");
    }

    void Update()
    {
        Swarm();
    }

    // needed?
    // private void SetEnemyValues()
    // {
    //     GetComponent<Health>().SetHealth(data.hp, data.hp);
    //     damage = data.damage;
    //     speed = data.speed;
    // }

    void DebugDrawCircle(Vector3 center, float radius, Color color, float duration = 0.5f, int segments = 16)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(Mathf.Cos(0), Mathf.Sin(0)) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Debug.DrawLine(prevPoint, nextPoint, color, duration);
            prevPoint = nextPoint;
        }
    }


    public void Attack(){
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRadius, defaultLayerMask);
        DebugDrawCircle(transform.position, hitRadius, Color.red, 0.5f);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform) continue;
            ITakeDamage player = hit.GetComponent<ITakeDamage>();
            if (player != null)
                player.TakeDamage(damage);
        }
        swingAnimator.SetTrigger("attack");
    }

    private void Swarm()
    {
        GameObject target = baseObject;

        if (playerObject)
        {
            float playerDistance = Vector2.Distance(transform.position, playerObject.transform.position);

            // If the player is within detection range, switch target to the player
            if (playerDistance <= detectionRange)
            {
                target = playerObject;
            }
        }

        if (target)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            Vector3 scale = transform.localScale;
            if (target.transform.position.x < transform.position.x)
                scale.x = -Mathf.Abs(scale.x); // face left
            else
                scale.x = Mathf.Abs(scale.x); // face right
            transform.localScale = scale;

            // Move towards the target if not within stopDistance
            if (distance > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else if (target == playerObject) // Attack only if the target is the player
            {
                Attack();
            }
        }
    }

    public void IncreaseDmg(float amount)
    {
        damage += (int)amount;
        Debug.Log($"Enemy damage increased to: {damage}");
    }


}
