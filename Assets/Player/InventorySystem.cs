using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Text[] slots = new Text[10];  // 10 UI text elements
    Item[] items = new Item[10];     // Inventory items
    int selectedSlot = 0;                // Currently selected slot

    void Update()
    {
        // Press number keys 1–0 to select slot 0–9
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i % 10)))
            {
                selectedSlot = i;
                Debug.Log("Selected slot: " + (i + 1));
            }
        }
    }

    public void PickupRandomItem()
    {
        if (null == (items[selectedSlot]))
        {
            string newItem = "Item" + Random.Range(1, 100);
            items[selectedSlot] = newItem;
            Debug.Log("Picked up: " + newItem);
            UpdateUI();
        }
        else
        {
            Debug.Log("Slot already full!");
        }
    }

    public void DropSelectedItem()
    {
        if (null != (items[selectedSlot]))
        {
            Debug.Log("Dropped: " + items[selectedSlot]);
            items[selectedSlot] = "";
            UpdateUI();
        }
        else
        {
            Debug.Log("No item to drop!");
        }
    }

    public void UseSelectedItem(){

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].text = items[i].name;
        }
    }
}
