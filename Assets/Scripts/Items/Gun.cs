using UnityEngine;

public class Gun : Item
{
    public float maxDistance = 100f;
    public LayerMask hitMask; // Set this in the Inspector (e.g. "Enemy" or "Resource")

    public Transform firePoint;
    public GameObject muzzleFlash;
    public float damage;

    public int bulletUse = 1;


    public override void Use(InventorySystem inventory)
    {
        if(inventory.CountItems("Bullet") < bulletUse) return;
        // use bullets if there is not enough canse the shooting
        for(int i = 0; i < bulletUse; i ++){
            Item bullet = inventory.PopItem("Bullet");
        }
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 origin = transform.position;
        Vector2 direction = (mouseWorldPos - transform.position);
        direction.Normalize();

        // Shoot ray
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, hitMask);

        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        GameObject flash = Instantiate(muzzleFlash, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        Destroy(flash, 0.2f); // Optional cleanup

        // Debug line
        Debug.DrawRay(origin, direction * maxDistance, Color.red, 0.5f);

        if (hit.collider != null) {
            Debug.Log("Hit: " + hit.collider.name);

            Resource res = hit.collider.GetComponent<Resource>();
            if (res != null) {
                res.TakeDamage(damage);
            }
        }

    }
}
