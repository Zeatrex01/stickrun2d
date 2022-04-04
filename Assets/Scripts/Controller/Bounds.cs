using UnityEngine;
using System.Collections;

public class Bounds : MonoBehaviour {
	

	void OnTriggerExit2D(Collider2D other){
		
		switch (other.gameObject.tag) {
		case "Player":
			Destroy(other.gameObject);	
			GameObject.Find ("GamePlayController").GetComponent<GamePlayController> ().gameRunning = false;
			break;
		
		case "GroundBlock":
			Destroy(other.gameObject);	
			break;
		
		default:
			Destroy(other.gameObject);
			break;
		}
	}
}
