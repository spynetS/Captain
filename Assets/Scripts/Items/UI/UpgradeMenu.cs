using UnityEngine;
using System.Collections.Generic;

public class UpgradeMenu : Toggle
{

    public List<Item> itemsCanBeUpgraded = new List<Item>(); // the items that can be upgraded

    public Transform upgradeHolder;
    public GameObject upgradePrefab;

    public Base myBase;

    void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            this.UpdateMenu();
        }
    }


    public override void UpdateMenu(){
        this.CheckUpgradable();
        this.UpdateUi();
    }

    void CheckUpgradable(){
        itemsCanBeUpgraded.Clear();// clear before add
        // goes through all items and check if the items cost is in the inventory
        foreach(Stack<Item> stack in inventory.stacks){
            if(stack.Count > 0 && stack.Peek().nextUpgrade != null){
                //
                if(stack.Peek().CheckCost(stack.Peek().getCost(),inventory.GetAllItems())){
                    itemsCanBeUpgraded.Add(stack.Peek());
                }
            }
        }
        if(myBase.fenceItem != null && myBase.fenceItem.CheckCost(myBase.fenceItem.getCost(),inventory.GetAllItems())){
            itemsCanBeUpgraded.Add(myBase.fenceItem);
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
            if(item == myBase.fenceItem)
                uli.isFence = true;
            uli.SetItem(item,this);

        }
    }

}
