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
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
