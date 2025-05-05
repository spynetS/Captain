using UnityEngine;
using System.Collections.Generic;
using System.Linq; // For LINQ methods like Select



/**
 * Item is a object that can be picked up by a player
 * and used. This is mostly a abstract version to be implemented
 * but can be used to unsusable items.
 *
 * How to upgrade
 * *  */
public class Item : YSort
{
    public GameObject nextUpgrade;
    public List<Item> cost = new List<Item>();
    public string name = "";
    /**
     * Method that starts uses an item. Abstract
     *  */
    public virtual void Use(InventorySystem inventory)
    {

    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            this.Upgrade(cost);
        }
    }

    private bool CheckCost(List<Item> cost) {
        List<Item> remainingOffers = new List<Item>(cost); // Clone to track used items

        foreach (Item requiredItem in this.cost) {
            bool matched = false;
            for (int i = 0; i < remainingOffers.Count; i++) {
                if (remainingOffers[i] != null && remainingOffers[i].name == requiredItem.name) {
                    remainingOffers.RemoveAt(i);
                    matched = true;
                    break;
                }
            }
            if (!matched) {
                return false; // requiredItem not found in offers
            }
        }

        return true; // all required items matched
    }


    /**
     * Method to handle upgrading of items. If the
     * cost is the same as the cost then the item
     * gets repalced with the `nextUpgrade` item.
     *  */
    public GameObject Upgrade(List<Item> cost){
        if(nextUpgrade){
            if(CheckCost(cost)){
                foreach(Item v in cost){
                    //Destroy(v);
                }
                GameObject newGameObject = Instantiate(nextUpgrade, transform.position, transform.rotation, gameObject.transform.parent);
                Destroy(gameObject);
                return newGameObject;
            }
        }
        return null;
    }
    /** Returns the cost to be upgraded */
    public List<Item> getCost(){
        return this.cost;
    }

}
