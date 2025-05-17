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

        currentHealth -= (int) damage;
        UpdateBar();

        AudioSource.PlayClipAtPoint(hitClip, transform.position);

    }

    public void UpdateBar()
    {
        if (healthBarFill != null)
        {
            float fill = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = fill;

            // Blend from green to yellow to red
            Color healthyColor = Color.green;
            Color mediumColor = Color.yellow;
            Color lowColor = Color.red;

            if (fill > 0.5f)
            {
                // From Green to Yellow
                float t = (fill - 0.5f) / 0.5f; // 1 at full, 0 at 50%
                healthBarFill.color = Color.Lerp(mediumColor, healthyColor, t);
            }
            else
            {
                // From Yellow to Red
                float t = fill / 0.5f; // 1 at 50%, 0 at 0%
                healthBarFill.color = Color.Lerp(lowColor, mediumColor, t);
            }
        }
    }
}
