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

    public override void TakeDamage(float dmg)
    {
        this.health -= dmg;
        if (this.health <= 0)
        {
            GameManager.Instance.BaseDestroyed();
            Destroy(gameObject);
        }
    }
}
