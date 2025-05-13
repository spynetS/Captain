using UnityEngine;
using System.Collections;
using System;

// public class EnemyWallBreaker : MonoBehaviour
// {
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Wall"))
//         {
//             Debug.Log("wall has been broken");
//             Destroy(other.gameObject); // Destroys the wall
//         }
//     }
// }


public class EnemyWallBreaker : MonoBehaviour
{
    public string targetTag = "Wall";
    public float attackDelay; // Delay between attacks
    public float dmgPerHit; // Damage dealt per hit
    private Resource targetWall;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private EnemyAI enemyAI; // Reference to the EnemyAI script, ADDED FOR TESTING
    private Enemy me;

    private Rigidbody2D rb;

    void Start()
    {
        me = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>(); // ADDED FOR TESTING
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(targetTag))
        {
            Resource res = other.GetComponent<Resource>();
            if (res != null)
            {
                targetWall = res;
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(AttackWall());
                }
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            if(attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            attackCoroutine = null;
            targetWall = null;
            isAttacking = false;

            if (enemyAI != null)
            {
                enemyAI.isAttackingWall = false; // ADDED FOR TESTING
            }
        }
    }

    IEnumerator AttackWall()
    {
        isAttacking = true;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        if (enemyAI != null)
        {
            enemyAI.isAttackingWall = true;
        }

        while (targetWall != null)
        {
            if (me != null)
            {
                me.Attack();
            }
            // Call TakeDamage on the wall
            targetWall.TakeDamage(dmgPerHit);

            // Stop if wall was destroyed
            if (targetWall.health <= 0)
            {
                targetWall = null;
                break;
            }

            yield return new WaitForSeconds(attackDelay);
        }

        if (enemyAI != null)
        {
            enemyAI.isAttackingWall = false;
        }
        attackCoroutine = null;
        isAttacking = false;
    }

}
