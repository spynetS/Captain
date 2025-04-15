using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject sword;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = -999f;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        Move();

        if (Input.GetButtonDown("Fire1") && GetComponent<WeaponManager>().IsUsingGun())
        Shoot();

        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<WeaponManager>().IsUsingSword())
        SwingSword();
    }


    void SwingSword()
    {
    lastAttackTime = Time.time;
    sword.SetActive(true);
    Invoke("DisableSword", 0.2f); // Sword is active for 0.2 sec
    }

    void DisableSword()
    {
    sword.SetActive(false);
    }



    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
