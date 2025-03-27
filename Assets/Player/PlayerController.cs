using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;

    [Header("Equipped Item")]
    [SerializeField] private GameObject equippedItem; // Item assigned in Inspector or code

    public GameObject EquippedItem => equippedItem; // Read-only access from other scripts (optional)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleActionInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void HandleMovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void HandleActionInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseEquippedItem();
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    private void UseEquippedItem()
    {
        if (equippedItem != null)
        {
            IUsable usable = equippedItem.GetComponent<IUsable>();
            if (usable != null)
            {
                usable.Use();
            }
            else
            {
                Debug.LogWarning("Equipped item has no IUsable component.");
            }
        }
        else
        {
            Debug.Log("No item equipped.");
        }
    }

    // Optional: Method to equip a new item via code
    public void EquipItem(GameObject newItem)
    {
        equippedItem = newItem;
    }
}
