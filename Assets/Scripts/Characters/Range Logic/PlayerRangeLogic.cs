using UnityEngine;
using System.Collections;

public class PlayerRangeLogic : MonoBehaviour {

	private BasePlayer player;
	
	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<BasePlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			player.EnemyEnteredRange(other.GetComponent<BaseEnemyAI>());
		}	
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag == "Enemy") {
			player.EnemyLeftRange(other.GetComponent<BaseEnemyAI>());
		}
	}
}
