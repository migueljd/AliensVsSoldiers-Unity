using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	BasePlayer player;
	Camera mainCamera;
	Vector3 limbo = new Vector3 (99999, 99999, 99999);

	Vector3 goal;

	public LayerMask raycastLayerToIgnore;



	void Awake(){
	}

	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag("Player").GetComponent<BasePlayer>();
		mainCamera = Camera.main;
		goal = limbo;
	}
	
	// Update is called once per frame
	void Update () {

		//-------------------------------------------------------------------------------------------------------------------
		//TOUCH LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		if (Input.touchCount > 0) {


			RaycastHit hit;
			Vector2 touchPosition =Input.GetTouch(0).position;
			Ray ray = mainCamera.ScreenPointToRay(touchPosition);


			if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~(raycastLayerToIgnore))){

				Debug.Log (hit.collider.name);

				//Player should move torwards that point
				if(hit.collider.tag == "Level"){
					goal = hit.point;

				} 
				//Player should have that object as the new target
				else if(hit.collider.tag == "Enemy"){
					Debug.Log ("Target acquired");
					Debug.Log (hit.collider.name);
					player.target = hit.collider.GetComponent<BaseEnemy>();
					goal = limbo;
				}
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//MOVEMENT LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		if (!goal.Equals (limbo)  || player.target != null) {
			if(!goal.Equals (limbo)){
				player.MoveTowards(goal);

			} else if(player.target != null ){
				player.MoveTowards(player.target.transform.position);
			}
		}


		//-------------------------------------------------------------------------------------------------------------------
		//ATTACK LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		if (player.enemiesInRange.Count > 0) {

			bool targetInRange = false;
			player.UnassignTargetDelegate();

			foreach (BaseEnemy enemy in player.enemiesInRange) {
				if (enemy.Equals (player.target)) {
					player.nextEnemyToAttack = enemy;
					targetInRange = true;
				}
			}

			if (!targetInRange) {
				player.nextEnemyToAttack = player.enemiesInRange [0];
			}

			player.AssignTargetDelegate();
			
			player.StartAttackAnimation ();

		} else{

			player.nextEnemyToAttack = null;
		}
		
	}




}
