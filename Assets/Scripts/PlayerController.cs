using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	BasePlayer player;
	Camera mainCamera;
	Vector3 limbo = new Vector3 (99999, 99999, 99999);

	Vector3 goal;


	public float touchRaycastRange = 10;
	

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

			Physics.Raycast(ray, out hit);

<<<<<<< HEAD
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~(raycastLayerToIgnore))){

=======
>>>>>>> origin/master

			//Player should move torwards that point
			if(hit.collider.tag == "Level"){
				goal = hit.point;

<<<<<<< HEAD
				} 
				//Player should have that object as the new target
				else if(hit.collider.tag == "Enemy"){

					player.target = hit.collider.GetComponent<BaseEnemy>();
					goal = limbo;
				}
=======
			} 
			//Player should have that object as the new target
			else if(hit.collider.tag == "Enemy"){
				player.target = hit.collider.GetComponent<BaseEnemy>();
				goal = limbo;
>>>>>>> origin/master
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//MOVEMENT LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		if ((!goal.Equals (limbo) && goal != null) || player.target != null) {
			if(!goal.Equals (limbo)){
				player.MoveTowards(goal);

			} else if(player.target != null ){
				player.MoveTowards(player.target.transform.position);
			}
		}


		//-------------------------------------------------------------------------------------------------------------------
		//ATTACK LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		Debug.Log (player.enemiesInRange.Count);
		if (player.enemiesInRange.Count > 0) {

			bool targetInRange = false;

			foreach(BaseEnemy enemy in player.enemiesInRange){
				if(enemy.Equals(player.target)){
					player.nextEnemyToAttack = enemy;
					targetInRange = true;
				}
			}

			if(!targetInRange){
				player.nextEnemyToAttack = player.enemiesInRange[0];
			}

			player.StartAttackAnimation();
		}
		
	}




}
