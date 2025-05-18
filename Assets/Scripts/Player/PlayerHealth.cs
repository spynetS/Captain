using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int maxHealth = 10;
    public int currentHealth;
    public Image healthBarFill;

    private float damageCooldown = 1f;
    private float lastDamageTime = -999f;

    public AudioClip hitClip;

    // Respawn coordinates
    private Vector2 respawnPosition = new Vector2(10f, -11f);

    void Start()
    {
        currentHealth = maxHealth;
        UpdateBar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
            TakeDamage(10);
    }

    public void TakeDamage(float damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        lastDamageTime = Time.time;

        currentHealth -= (int)damage;
        UpdateBar();

        AudioSource.PlayClipAtPoint(hitClip, transform.position);

        // Check for death
        if (currentHealth <= 0)
        {
            RespawnPlayer();
        }
    }

    public void UpdateBar()
    {
        if (healthBarFill != null)
        {
            float fill = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = fill;

            Color healthyColor = Color.green;
            Color mediumColor = Color.yellow;
            Color lowColor = Color.red;

            if (fill > 0.5f)
            {
                float t = (fill - 0.5f) / 0.5f;
                healthBarFill.color = Color.Lerp(mediumColor, healthyColor, t);
            }
            else
            {
                float t = fill / 0.5f;
                healthBarFill.color = Color.Lerp(lowColor, mediumColor, t);
            }
        }
    }

    private void RespawnPlayer()
    {
        transform.position = respawnPosition; // Move player to respawn point
        currentHealth = maxHealth;            // Reset health
        UpdateBar();                          // Update UI
    }
}
