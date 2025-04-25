using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);

            // Apply knockback
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockDir = (other.transform.position - transform.position).normalized;
                rb.linearVelocity = Vector2.zero; // Stop any current movement
                rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
                StartCoroutine(StopAfterDelay(rb));
            }
        }
    }

    System.Collections.IEnumerator StopAfterDelay(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(knockbackDuration);
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}

