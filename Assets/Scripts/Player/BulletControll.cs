using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public int baseDamage = 1;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Find player to check if Strength buff is active
            GameObject player = GameObject.FindWithTag("Player");
            int finalDamage = baseDamage;

            if (player != null)
            {
                PlayerStatus status = player.GetComponent<PlayerStatus>();
                if (status != null && status.HasStrength())
                {
                    finalDamage *= 2; // Double damage if Strength buff is active
                }
            }

            other.GetComponent<Health>().TakeDamage(finalDamage);
            Destroy(gameObject);
        }
    }
}
