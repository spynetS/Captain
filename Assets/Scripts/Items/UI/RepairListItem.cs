using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class RepairListItem : MonoBehaviour {

    public List<Item> cost = new List<Item>();
    public InventorySystem inventory;
    public TextMeshProUGUI text;
    public Action lambda;
    public Button button;

      public bool CheckCost(List<Item> required, List<Item> offers) {
        // Count required quantities
        var neededCounts = new Dictionary<string,int>();
        foreach (var need in required) {
            if (!neededCounts.TryGetValue(need.name, out var cnt)) cnt = 0;
            neededCounts[need.name] = cnt + 1;
        }

        // Count offer quantities
        var offerCounts = new Dictionary<string,int>();
        foreach (var offer in offers) {
            if (!offerCounts.TryGetValue(offer.name, out var cnt)) cnt = 0;
            offerCounts[offer.name] = cnt + 1;
        }

        // Ensure every required count is met
        foreach (var kv in neededCounts) {
            if (!offerCounts.TryGetValue(kv.Key, out var have) || have < kv.Value)
                return false;
        }
        return true;
    }


    private void DestroyCost(InventorySystem inventory, List<Item> cost) {
        List<Item> remainingOffers = new List<Item>(cost); // Clone to track used items

        foreach (Item requiredItem in this.cost) {
            for (int i = 0; i < remainingOffers.Count; i++) {
                if (remainingOffers[i] != null && remainingOffers[i].name == requiredItem.name) {
                    inventory.DestroyItem(remainingOffers[i]);
                    remainingOffers.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void Update(){
        button.interactable = CheckCost(this.cost,inventory.GetAllItems());
    }

    public void Pressed(){
        if(CheckCost(this.cost,inventory.GetAllItems())){
            this.lambda();
            DestroyCost(inventory,inventory.GetAllItems());
        }

    }

}
