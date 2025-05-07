using UnityEngine;

public class Egg : MonoBehaviour
{
    public float life = 1f;

    void Awake()
    {
        StartCoroutine(DestroyAfterTime());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Egg"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            // what else we want here ????
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
