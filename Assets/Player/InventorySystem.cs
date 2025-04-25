using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Image[] slots = new Image[10];  // 10 UI text elements
    public Item[] items = new Item[10];     // Inventory items
    private int selectedSlot = 0;

    public Transform hand;// Currently selected slot
    public Sprite emptySlot;
    public Sprite selectedSlotSprite;

    public Collider2D collider;

    void Update()
    {
        // Press number keys 1–0 to select slot 0–9
        for (int i = 0; i < 10; i++)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1 + (i % 10))){
                selectedSlot = i;
            }
            else{
                Item item = items[i];
                if(item != null){
                    item.transform.gameObject.SetActive(selectedSlot == i ? true : false);
                    slots[i].sprite = selectedSlot == i ? selectedSlotSprite : emptySlot;
                }
            }
        }
        UpdateUI();
    }


    public void DropSelectedItem()
    {
        if (null != (items[selectedSlot]))
        {
            Item item = items[selectedSlot];

            // drop the item to the ground
            GameObject dropped = item.gameObject;
            dropped.transform.SetParent(null);
            dropped.SetActive(true);
            Vector2 randomDirection = Random.insideUnitCircle.normalized/2;
            float force = 5f; // try something noticeable
            dropped.GetComponent<Rigidbody2D>().AddForce(randomDirection * force, ForceMode2D.Impulse);

            items[selectedSlot] = null;
            UpdateUI();
        }
    }

    public void UseSelectedItem(){
        Item item = items[selectedSlot];
        if(item != null){
            item.Use();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++){
            Item item = items[i];
            if (item != null)
            {
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
                Image childImage = slots[i].transform.GetChild(0).GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.sprite = emptySlot;
                }
            }

        }
    }

    private int GetEmptySlotIndex(){
        for(int i = 0; i < 10; i ++){
            if(items[i] == null) return i;
        }
        return -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item.transform.parent == this.hand) return;

        if(item != null){
            int empty = GetEmptySlotIndex();
            if(empty == -1) return;

            items[empty] = item;
            item.transform.SetParent(hand);
            item.transform.localPosition = Vector3.zero;
            //item.transform.position = Vector3.zero;
        }
    }

}
