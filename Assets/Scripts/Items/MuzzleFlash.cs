using UnityEngine;

public class MuzzleFlash :  MonoBehaviour {

	SpriteRenderer renderer;

	void Start(){
		renderer = this.transform.GetComponentInChildren<SpriteRenderer>();
	}

	void Update() {
		if(renderer.gameObject.active == false){
			Destroy(this.transform);
		}
	}

}
