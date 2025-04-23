using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Text[] slots = new Text[10];  // 10 UI text elements
    public Item[] items = new Item[10];     // Inventory items
    int selectedSlot = 0;
    public Transform hand;// Currently selected slot

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
                }
            }
        }
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
            float force = 2f; // try something noticeable
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
        for (int i = 0; i < slots.Length; i++)
        {
            Item item = items[i];
            if(item != null){
                slots[i].text = item.name;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item != null){
            items[0] = item;
            item.transform.SetParent(hand, false);
            item.transform.localPosition = Vector3.zero;
        }
    }

}
