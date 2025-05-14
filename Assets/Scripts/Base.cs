using UnityEngine;

public class Base : Resource {

	public int fenceUpgrade = 0;
	public FenceLocation fenceLocation;

	public Item fenceItem;


	public void Upgrade(){
		if(fenceUpgrade == 0){
			fenceLocation.PlaceWalls();
		}
		else{
			fenceLocation.UpgradeWalls(fenceUpgrade);
		}
		fenceUpgrade ++;
	}
}
