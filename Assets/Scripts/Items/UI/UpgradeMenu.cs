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
            this.UpdateUi();
        }
    }

    void UpdateUi(){
        foreach(Item item in this.itemsCanBeUpgraded){
        GameObject newUpgrade = Instantiate(this.upgradePrefab, upgradeHolder);

        // Optionally reset its local scale and position
        newUpgrade.transform.localScale = Vector3.one;
        newUpgrade.transform.localPosition = Vector3.zero;

        UpgradeListItem uli = newUpgrade.GetComponent<UpgradeListItem>();

        uli.SetItem(item);

        }
    }

}
