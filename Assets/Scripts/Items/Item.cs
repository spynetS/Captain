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

    // New: Cooldown settings
    [Header("Use Rate Settings")]
    [Tooltip("How many times per second this item can be used")]
    public float usesPerSecond = 1f;
    public bool canHold = true;
    protected float lastUseTime = -Mathf.Infinity;

    public virtual void Use(InventorySystem inventory){}

    public AudioSource audioSource;
    public AudioClip clip;

    void Start(){
        if(audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 0f; // Makes it 2D
    }


    public void CallUse(InventorySystem inventory)
    {
        if (CanUse(inventory)) {
            lastUseTime = Time.time;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            this.Use(inventory);
        }
    }

    protected virtual bool CanUse(InventorySystem inventory)
    {
        return Time.time >= lastUseTime + (1f / usesPerSecond);
    }

    void LateUpdate(){
        if(transform.parent != null && transform.parent.tag == "hand"){
            this.UpdateOrder(transform.parent.GetComponentInParent<SpriteRenderer>().sortingOrder-1);
        }
        else{
            this.UpdateOrder();
        }
    }

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


    /**
     * Method to handle upgrading of items. If the
     * cost is the same as the cost then the item
     * gets repalced with the `nextUpgrade` item.
     *  */
    public GameObject Upgrade(InventorySystem inventory, List<Item> cost){
        if(nextUpgrade){
            if(CheckCost(this.cost,cost)){
                this.DestroyCost(inventory,cost);
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
