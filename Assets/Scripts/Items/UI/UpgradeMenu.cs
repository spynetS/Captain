using UnityEngine;
using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{

    private InventorySystem inventory;
    public List<Item> itemsCanBeUpgraded = new List<Item>(); // the items that can be upgraded

    public Transform upgradeHolder;
    public GameObject upgradePrefab;

    void Start(){
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            this.UpdateMenu();
        }
    }

    public void UpdateMenu(){
        this.CheckUpgradable();
        this.UpdateUi();
    }

    void CheckUpgradable(){
        itemsCanBeUpgraded.Clear();// clear before add
        foreach(Stack<Item> stack in inventory.stacks){
            if(stack.Count > 0 && stack.Peek().nextUpgrade != null){
                if(stack.Peek().CheckCost(stack.Peek().getCost(),inventory.GetAllItems())){
                    itemsCanBeUpgraded.Add(stack.Peek());
                }
            }
        }
    }

    private void UpdateUi(){
        // clear the elements before add
        foreach (Transform child in upgradeHolder.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Item item in this.itemsCanBeUpgraded){
            GameObject newUpgrade = Instantiate(this.upgradePrefab, upgradeHolder);
            // Optionally reset its local scale and position
            newUpgrade.transform.localScale = Vector3.one;
            newUpgrade.transform.localPosition = Vector3.zero;

            UpgradeListItem uli = newUpgrade.GetComponent<UpgradeListItem>();

            uli.SetItem(item,this);

        }
    }

}
