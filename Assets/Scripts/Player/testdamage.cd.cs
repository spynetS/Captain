using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damageAmount = 1;
    public KeyCode damageKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(damageKey))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

