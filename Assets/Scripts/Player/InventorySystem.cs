using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class InventorySystem : MonoBehaviour
{
    public Image[] slots = new Image[10];  // 10 UI text elements
    public Stack<Item>[] stacks = new Stack<Item>[10];
    public int selectedSlot = 0;

    public Transform hand;// Currently selected slot
    public Sprite emptySlot;
    public Sprite normalSlot;
    public Sprite selectedSlotSprite;

    public Collider2D collider;
    public AudioClip clip;

    void Start(){

        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<Item>();
        }
    }

    public int GetStackIndexByItemName(string targetName)
    {
        for (int i = 0; i < 10; i++)
        {
            if (this.stacks[i].Count > 0 && this.stacks[i].Peek().name == targetName)
            {
                return i;
            }
        }

        // Not found
        return -1;
    }


    void Update()
    {
        // Scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedSlot = (selectedSlot - 1 + 10) % 10; // Scroll up, wrap correctly
        }
        else if (scroll < 0f)
        {
            selectedSlot = (selectedSlot + 1) % 10; // Scroll down, wrap correctly
        }
        // Press number keys 1–0 to select slot 0–9
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i % 10)))
            {
                selectedSlot = i;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                selectedSlot = 9;
            }


            for (int j = 0; j < stacks.Length; j++)
            {
                foreach (Item item in stacks[j])
                {
                    if (item != null)
                        item.transform.gameObject.SetActive(false); // Turn off all items
                }

                // Enable only the top item in the selected slot
                if (j == selectedSlot && stacks[j].Count > 0)
                {
                    Item topItem = stacks[j].Peek();
                    if (topItem != null)
                        topItem.transform.gameObject.SetActive(true);
                }
            }


            if(selectedSlotSprite && normalSlot){
                slots[i].sprite = selectedSlot == i ? selectedSlotSprite : normalSlot;
            }


        }

        UpdateUI();
    }

    public void DropSelectedItem()
    {
        if (stacks[selectedSlot].Count > 0)
        {
            Item item = stacks[selectedSlot].Pop();

            // Drop the item into the world
            GameObject dropped = item.gameObject;
            dropped.transform.SetParent(null);
            dropped.SetActive(true);


            // Set the drop position to the player's position
            Vector3 playerPos = transform.position;

            // Get mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // Calculate direction from player to mouse
            Vector3 direction = (mousePos - playerPos).normalized;

            dropped.transform.position = playerPos+direction;


            // Apply force to "throw" the item
            float force = 2f;
            Rigidbody2D rb = dropped.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; // Clear any existing velocity
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }

            UpdateUI();
        }
    }

    public void DestroyItem(Item item)
    {
        // Remove item from stack
        for (int i = 0; i < stacks.Length; i++)
        {
            if (stacks[i].Contains(item))
            {
                var tempStack = new Stack<Item>();
                while (stacks[i].Count > 0)
                {
                    Item popped = stacks[i].Pop();
                    if (popped != item)
                    {
                        tempStack.Push(popped);
                    }
                }

                // Rebuild the stack without the item
                while (tempStack.Count > 0)
                {
                    stacks[i].Push(tempStack.Pop());
                }

                break;
            }
        }

        Destroy(item.gameObject);
    }

    /**
     * This function will upgrade the item
     * at the @index slot
     * with the @cost gameobjects
     *
     * */
    public void UpgradeItemAt(int index, List<Item> cost){
        if(this.stacks[index].Count > 0){
            Item item = this.stacks[index].Pop();
            if(item.nextUpgrade){
                GameObject newGO = item.Upgrade(this,cost);
                if(newGO != null){
                    // push the upgraded item to the inventory
                    Item newItem = newGO.GetComponent<Item>();
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localScale = new Vector3(1,1,1);
                    newItem.transform.localRotation = Quaternion.identity;
                    this.stacks[this.GetEmptySlotIndex(newItem)].Push(newItem);
                }
                else{
                    this.stacks[index].Push(item);
                }
            }
            else{
                this.stacks[index].Push(item);
            }
        }
    }

    public void UseSelectedItem(){
        if(stacks[selectedSlot].Count > 0){
            Item item = stacks[selectedSlot].Peek();
            if(item != null){
                Debug.Log(item);
                item.CallUse(this);
            }
        }
        UpdateUI();
    }

    public Item GetSelectedItem(){
        if(stacks[selectedSlot].Count > 0){
            Item item = stacks[selectedSlot].Peek();
            if(item != null){
                return item;
            }
        }
        return null;
    }


    public List<Item> GetAllItems(){
        List<Item> cost = new List<Item>();
        foreach(Stack<Item> stack in this.stacks){
            foreach(Item item in stack){
                cost.Add(item);
            }
        }
        return cost;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++){

            if(slots[i] == null) return;

            Text childText = slots[i].GetComponentInChildren<Text>();
            if(childText != null){
                childText.text = stacks[i].Count.ToString();
            }

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
                    childImage.preserveAspect = true;
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

    public Item PopItem(string name){
        foreach(Stack<Item> stack in stacks){
            if(stack.Count > 0 && stack.Peek().name == name){
                return stack.Pop();
            }
        }
        return null;
    }

     public int CountItems(string name){
        foreach(Stack<Item> stack in stacks){
            if(stack.Count > 0 && stack.Peek().name == name){
                return stack.Count;
            }
        }
        return 0;
    }


    private int GetEmptySlotIndex(Item item){
        // we loop through and if there is an empty we save it
        // if there is a stack with the same item we return it directly
        // else we return the empty we found
        int empty = -1;
        for(int i = 0; i < 10; i ++){
            if(empty == -1 && stacks[i].Count == 0) empty = i;
            if (stacks[i].Count > 0 && stacks[i].Peek().name == item.name) return i;
        }


        return empty; // dangerus hehe
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
            //item.transform.localScale = new Vector3(1,1,1);
            item.transform.localRotation = Quaternion.identity;
            //item.transform.position = Vector3.zero;
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

    }

}
