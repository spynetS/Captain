using UnityEngine;

public class Gun : Item
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public override void Use(InventorySystem inventory)
    {
        Debug.Log("Firing gun!");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Vi kan lägga till Effekter senare
    }
}
