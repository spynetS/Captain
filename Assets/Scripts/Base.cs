using UnityEngine;
using System.Collections.Generic;

public class Base : Resource {

	public int fenceUpgrade = 0;
	public FenceLocation fenceLocation;

	public Item fenceItem;
    public Item oldFenceItem;

    public List<Item> repairCost = new List<Item>();


	public void Upgrade(){
		if(fenceUpgrade == 0){
			fenceLocation.PlaceWalls();
		}
		else{
			fenceLocation.UpgradeWalls(fenceUpgrade);
		}
		fenceUpgrade ++;
	}

    public override void TakeDamage(float dmg)
    {
		if(healthCanvas){
            healthCanvas.SetActive(true);
        }
        this.health -= dmg;
        if (this.health <= 0)
        {
            GameManager.Instance.BaseDestroyed();
            Destroy(gameObject);
        }
    }
}
