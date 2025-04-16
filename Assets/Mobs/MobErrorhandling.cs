using UnityEngine;

public class ErrorHandling : MonoBehaviour
{
    private Enemy enemy;
    private Health getHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Try to find the Enemy component on this GameObject
        enemy = GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("Error: Enemy component is missing on this GameObject!");
        }
        else
        {
            // Try to find the Resource component on this GameObject
            getHealth = GetComponent<Health>();
            if (getHealth == null)
            {
                Debug.LogError("Error: health component is missing on this GameObject!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null && getHealth != null)
        {
            // Example: Check if the player object is assigned
            if (enemy.PlayerObject == null)
            {
                Debug.LogWarning("Warning: Player object is not assigned in the Enemy script!");
            }

            // Example: Check the health of the resource
            if (getHealth.health > 0)
            {
                Debug.Log("mob still has" + getHealth.health + " health left.");
            }
            else
            {
                Debug.Log("mob is dead.");
            }
        }
    }
}