using UnityEngine;
using System.Collections;

public class EnemyRangeLogic : MonoBehaviour {

	private BaseEnemy enemy;

	// Use this for initialization
	void Start () {
		enemy = transform.parent.GetComponent<BaseEnemy> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			enemy.PlayerEnteredRange(other.GetComponent<BasePlayer>());
		}	
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			enemy.PlayerLeftRange();
		}
	}
}
