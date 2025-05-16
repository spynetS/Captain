using UnityEngine;
using System.Collections;


public class Tool : Item, IGiveDamage
{
    public float damage;
    private float timer = 0;
    private float maxTimer = 10;
    private Coroutine swingCoroutine;


    public float hitRadius = 0.5f;
    public LayerMask hitMask; // Set this in the Inspector (e.g. "Enemy" or "Resource")


    public float GiveDamage()
    {
        return damage;
    }

    public void Update()
    {
        //RotateTowardsMouse();
        //FlipTowardsMouse();
    }

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


    public override void Use(InventorySystem inventory)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRadius, hitMask);
        DebugDrawCircle(transform.position, hitRadius, Color.red, 0.5f);
        foreach (Collider2D hit in hits)
        {
            Resource res = hit.GetComponent<Resource>();
            if (res != null)
                res.TakeDamage(damage);
        }
        SwingEffect();
    }

    private void FlipTowardsMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPos - transform.position;

        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x) * (direction.x >= 0 ? 1 : -1);
        transform.localScale = scale;
    }


    private void SwingEffect()
    {
        // find player and swingAnimator and run attack on it
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Animator animator = playerObject.GetComponent<PlayerController>().hand.gameObject.GetComponent<Animator>();
        animator.SetTrigger("attack");
    }

}
