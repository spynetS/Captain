using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform hand;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = -999f;
    public Animator animator;
    public GameObject swingEffect;


    public InventorySystem inventory; // reference to the InventorySystem
    public AudioClip footstepClip;
    public float footstepInterval = 0.1f; // How often to play a step sound

    private float footstepTimer = 0f;

    public UpgradeMenu upgradeMenu;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        Move(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        if(upgradeMenu != null && !upgradeMenu.show){
            if(inventory.GetSelectedItem() != null && inventory.GetSelectedItem().canHold && Input.GetKey(KeyCode.Mouse0)){
                inventory.UseSelectedItem();
            }
            else if(inventory.GetSelectedItem() != null && Input.GetKeyDown(KeyCode.Mouse0)){
                inventory.UseSelectedItem();
            }
        }

        if(Camera.main){
            // Get mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Ensure Z doesn't affect 2D

            // Get direction from effect to mouse
            Vector3 direction = mousePos - swingEffect.transform.position;

            // Calculate angle and apply rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            swingEffect.transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        }

        if (Input.GetKeyDown(KeyCode.E)){
            // upgrade the selected slot with the created list
            inventory.UpgradeItemAt(inventory.selectedSlot,inventory.GetAllItems());
        } //

        if (Input.GetKeyDown(KeyCode.Q))
            inventory.DropSelectedItem(); // calls inventory drop


    }

    public void Move(float x, float y)
    {
        Vector2 movement = new Vector2(x, y).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Animation update
        if (animator)
        {
            animator.SetFloat("MoveX", x);
            animator.SetFloat("MoveY", y);
            animator.SetBool("IsMoving", movement.magnitude > 0.01f);
        }

        // Flip sprite
        if (x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if (footstepClip != null && movement.magnitude > 0.01f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                AudioSource.PlayClipAtPoint(footstepClip, transform.position);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f; // Reset when idle
        }
    }


}
