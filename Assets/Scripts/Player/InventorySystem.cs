using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class InventorySystem : MonoBehaviour
{
    public Image[] slots = new Image[10];  // 10 UI text elements
    public Stack<Item>[] stacks = new Stack<Item>[10];
    private int selectedSlot = 0;

    public Transform hand;// Currently selected slot
    public Sprite emptySlot;
    public Sprite normalSlot;
    public Sprite selectedSlotSprite;

    public Collider2D collider;

    void Start(){
        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<Item>();
        }
    }

    void Update()
    {
        // Scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedSlot = (selectedSlot - 1) % 10; // Scroll up
        }
        else if (scroll < 0f)
        {
            selectedSlot = (selectedSlot + 1 + 10) % 10; // Scroll down (wraps around)
        }

        // Press number keys 1–0 to select slot 0–9
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i % 10)))
            {
                selectedSlot = i;
            }

            if (stacks[i].Count > 0)
            {
                Item item = stacks[i].Peek();
                if(item != null)
                    item.transform.gameObject.SetActive(selectedSlot == i);
            }
            if(selectedSlotSprite && normalSlot)
                slots[i].sprite = selectedSlot == i ? selectedSlotSprite : normalSlot;
        }

        UpdateUI();
    }


    public void DropSelectedItem()
    {
        if (stacks[selectedSlot].Count > 0)
        {
            Item item = stacks[selectedSlot].Pop();

            // drop the item to the ground
            GameObject dropped = item.gameObject;
            dropped.transform.SetParent(null);
            dropped.SetActive(true);
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized/2;
            float force = 5f; // try something noticeable
            dropped.GetComponent<Rigidbody2D>().AddForce(randomDirection * force, ForceMode2D.Impulse);

            UpdateUI();
        }
    }

    public void UseSelectedItem(){
        if(stacks[selectedSlot].Count > 0){
            Item item = stacks[selectedSlot].Pop();
            if(item != null){
                Debug.Log("USING");
                item.Use();

                if (item == null || item.gameObject == null){
                    Debug.Log("DESTROYED");
                }
                else{
                    stacks[selectedSlot].Push(item);
                }
            }
        }

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++){
            if (stacks[i].Count > 0)
            {
                Item item = stacks[i].Peek();

                // Get the sprite from the item's SpriteRenderer (or however you store it)
                Sprite itemSprite = item.transform.GetComponentInChildren<SpriteRenderer>()?.sprite;

                // Find the child Image under the slot
                Image childImage = slots[i].transform.GetChild(0).GetComponent<Image>();

                if (childImage != null && itemSprite != null)
                {
                    childImage.sprite = itemSprite;
                }
            }
            else
            {
                // Set to empty slot sprite if item is null
                try{
                    Image childImage = slots[i].transform.GetChild(0).GetComponent<Image>();
                    if (childImage != null && emptySlot)
                    {
                        childImage.sprite = emptySlot;
                    }
                }
                catch (Exception e){

                }
            }

        }
    }

    private int GetEmptySlotIndex(Item item){
        for(int i = 0; i < 10; i ++){
            if(stacks[i].Count == 0) return i;
            else if (stacks[i].Peek().name == item.name) return i;
        }
        return -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(!item || item.transform.parent == this.hand) return;

        if(item != null){
            int empty = GetEmptySlotIndex(item);
            if(empty == -1) return;

            stacks[empty].Push(item);// = item;
            item.transform.SetParent(hand);
            item.transform.localPosition = Vector3.zero;
            //item.transform.position = Vector3.zero;
        }
    }

}
