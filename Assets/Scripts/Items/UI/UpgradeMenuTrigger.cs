using UnityEngine;

public class UpgradeMenuTrigger : MonoBehaviour
{
	public UpgradeMenu upgradeMenu;

	void OnTriggerEnter2D(Collider2D other){
		if(upgradeMenu){
			upgradeMenu.ToggleMenu();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(upgradeMenu){
			upgradeMenu.ToggleMenu();
		}
	}
}
