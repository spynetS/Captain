using UnityEngine;

public class EnemyWallBreaker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("wall has been broken");
            Destroy(other.gameObject); // Destroys the wall
        }
    }
}
