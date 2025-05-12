using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeListItem : MonoBehaviour
{
    Item item;

    public Image itemImage;
    public Image nextItemImage;

    public GameObject costPrefab;
    public GameObject costList;

    // this will update the ui for the prefab when called
    public void SetItem(Item item){
        if(item.transform != null){
            itemImage.sprite = item.transform.GetComponentInChildren<SpriteRenderer>().sprite;
            itemImage.preserveAspect = true;

            nextItemImage.sprite = item.nextUpgrade.GetComponentInChildren<SpriteRenderer>().sprite;
            nextItemImage.preserveAspect = true;
        }

        foreach(Item cost in item.getCost()){
            GameObject newCost = Instantiate(costPrefab, costList.transform);

            // Optionally reset its local scale and position
            newCost.transform.localScale = Vector3.one;
            newCost.transform.localPosition = Vector3.zero;

            costPrefab.GetComponent<SpriteRenderer>().sprite = newCost.GetComponentInChildren<SpriteRenderer>().sprite;


        }


    }
    // this will upgrade the class item
    public void UpgradeItem(){

    }


}
