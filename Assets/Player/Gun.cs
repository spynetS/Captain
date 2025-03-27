using UnityEngine;

public class Gun : MonoBehaviour, IUsable
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public void Use()
    {
        Debug.Log("Firing gun!");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Vi kan lägga till Effekter senare
    }
}
