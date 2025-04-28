using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private Coroutine damageCoroutine;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Start damaging player every second
            damageCoroutine = StartCoroutine(DealDamageOverTime(collision.collider.GetComponent<Health>()));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Stop damaging when no longer touching
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
        }
    }

    IEnumerator DealDamageOverTime(Health playerHealth)
    {
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(2);
            yield return new WaitForSeconds(0.85f); // Damage interval
        }
    }
}
