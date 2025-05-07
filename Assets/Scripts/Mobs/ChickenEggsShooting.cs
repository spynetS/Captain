using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject eggPrefab;
    public Transform shootPoint; // Point from where the egg will be
    public float EggSpeed = 10;

    public Transform player; // Reference to the player transform
    public float shootTimerInterval = 0.5f; // interval between shots

    private float shootTimer;

    void Update()
    {
        shootTimer -= Time.deltaTime; // counter

        Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust rotation to face the player
    
        if(shootTimer <= 0f)
        {
            ShootEgg();
            shootTimer = shootTimerInterval; // Reset the timer
        }
    }

    void ShootEgg()
    {
        var egg = Instantiate(eggPrefab, shootPoint.position, shootPoint.rotation);
        egg.GetComponent<Rigidbody2D>().linearVelocity = shootPoint.up * EggSpeed;
    }
}
