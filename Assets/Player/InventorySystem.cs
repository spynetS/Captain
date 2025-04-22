using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Text[] slots = new Text[10];  // 10 UI text elements
    public Item[] items = new Item[10];     // Inventory items
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


    public void DropSelectedItem()
    {
        if (null != (items[selectedSlot]))
        {
            Item item = items[selectedSlot];

            GameObject dropped = item.gameObject;
            dropped.transform.SetParent(null);
            Vector2 randomDirection = Random.insideUnitCircle.normalized/2;

            float force = 1f; // try something noticeable
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
}
