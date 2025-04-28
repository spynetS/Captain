using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform hand;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = -999f;

    public InventorySystem inventory; // reference to the InventorySystem


    void Update()
    {
        if (Time.timeScale == 0f) return;

        Move(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            inventory.UseSelectedItem();
            Debug.Log("USE");
        }

        // Inventory controls
        if (Input.GetKeyDown(KeyCode.E))
        {
//            GameObject item = Instantiate()
        }


        if (Input.GetKeyDown(KeyCode.Q))
            inventory.DropSelectedItem(); // calls inventory drop
    }

    public void Move(float x, float y)
    {

        Vector2 movement = new Vector2(x, y);//.normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

}
