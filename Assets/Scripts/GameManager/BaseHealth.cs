using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            GameManager.Instance.BaseDestroyed();
            Destroy(gameObject);
        }
    }
}
