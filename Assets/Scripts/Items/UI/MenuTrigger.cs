using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
	public Toggle toggle;

	private Transform player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update(){
		float distance = Vector2.Distance(transform.position, player.position);
		if(toggle != null)
			toggle.ToggleIt(distance < 1);
	}

}
