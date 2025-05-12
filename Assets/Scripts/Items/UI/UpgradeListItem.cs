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
        Dictionary<string, GameObject> addedItems = new Dictionary<string, GameObject>();

        foreach(Item cost in item.getCost()){

            if(!addedItems.ContainsKey(cost.name)){
                GameObject newCost = Instantiate(costPrefab, costList.transform);
                newCost.transform.localScale = Vector3.one;
                newCost.GetComponent<Image>().sprite = cost.transform.GetComponentInChildren<SpriteRenderer>().sprite;
                //addedItems.Put(cost.name,newCost);
                addedItems[cost.name] = newCost;
            }

            var text = addedItems[cost.name].GetComponentInChildren<Text>();
            text.text = (int.Parse(text.text)+1).ToString();
        }


    }
    // this will upgrade the class item
    public void UpgradeItem(){

    }


}
