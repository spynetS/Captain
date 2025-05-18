using UnityEngine;
using System.Collections.Generic;

public class RepairMenu : Toggle{

    public Base myBase;

    public GameObject list;
    public GameObject listItem;

    public void RepairBase(){
        myBase.health = myBase.maxHealth;
        UpdateMenu();
    }

    public void RepairFence(){
        myBase.fenceLocation.PlaceWalls();
        myBase.fenceLocation.UpgradeWalls(myBase.fenceUpgrade-1);
    }

    public RepairListItem AddItem(string text, List<Item> cost){
        GameObject newUpgrade = Instantiate(this.listItem, list.transform);
        // Optionally reset its local scale and position
        newUpgrade.transform.localScale = Vector3.one;
        newUpgrade.transform.localPosition = Vector3.zero;

        RepairListItem rli = newUpgrade.transform.GetComponent<RepairListItem>();
        rli.text.text = text;
        rli.cost = cost;
        rli.inventory = inventory;
        return rli;
    }

    public override void UpdateMenu(){
        // clear the elements before add
        foreach (Transform child in list.transform) {
            GameObject.Destroy(child.gameObject);
        }

        if(myBase.health < myBase.maxHealth){
            AddItem("BASE",myBase.repairCost).lambda = () => RepairBase();
        }
        if(myBase.fenceLocation.AmountToRepair() > 0){
            AddItem("FENCE",myBase.oldFenceItem.getCost()).lambda = () => RepairFence();
        }


        // RepairListItem RLI = newUpgrade.transform.GetComponent<RepairListItem>();
        // RLI.lambda = () => fenceLocation.placePiller()
    }


}
