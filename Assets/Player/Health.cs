using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public Image healthBar;
    public string deathMessage;

    private float damageCooldown = 1f;
    private float lastDamageTime = -999f;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateBar();
    }

    public void TakeDamage(int damage)
    {
    if (Time.time - lastDamageTime < damageCooldown)
        return;

        lastDamageTime = Time.time;

        currentHealth -= damage;
        UpdateBar();

    if (currentHealth <= 0)
        {
        if (CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
        else if (CompareTag("Enemy"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Victory();
        }

        Destroy(gameObject);
        }
    }


    void UpdateBar()
    {
        if (healthBar != null)
        {
            float fill = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fill;
        }
    }
}

