using UnityEngine;

public class UpgradeMenuTrigger : MonoBehaviour
{
	public UpgradeMenu upgradeMenu;

	private Transform player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update(){
		float distance = Vector2.Distance(transform.position, player.position);
		upgradeMenu.ToggleMenu(distance < 2);

	}

//	void OnTriggerEnter2D(Collider2D other){
//		if(upgradeMenu && other.CompareTag("Player")){
//			upgradeMenu.ToggleMenu(true);
//		}
//	}
//
//	void OnTriggerExit2D(Collider2D other){
//		if(upgradeMenu && other.CompareTag("Player")){
//			upgradeMenu.ToggleMenu(false);
//		}
//	}
}
