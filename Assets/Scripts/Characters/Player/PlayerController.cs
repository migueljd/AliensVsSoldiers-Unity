using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	/*
	 * This class is used to control the player. 
	 * It's Update method is divided in 3 parts:
	 * 		TOUCH: check if a touch happened, and if it did, check if it was a goal or an enemy, and act accordingly
	 * 		MOVEMENT: Given there is a goal selected or an enemy, move according to that
	 * 		ATTACK: Check how the player is supposed to attack(in this case, a melee character that attacks anything that comes in range) and act
	 */


	//this delegate is used for whenever the player selects an enemy
	public delegate void EnemySelected(BaseEnemyAI enemy);
	public static EnemySelected onEnemySelected;

	//this delegate is used for whenever the player selects a new goal
	public delegate void GoalSelected(Vector3 goal);
	public static GoalSelected onGoalSelected;

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


			if(hit.collider != null){
				//Player should move torwards that point
				if(hit.collider.tag == "Level"){
					goal = hit.point;

					//if anyone is listening, let them know a new goal position has been selected
					if(onGoalSelected != null) onGoalSelected(goal);

				} 
				//Player should have that object as the new target
				else if(hit.collider.tag == "Enemy"){
					player.target = hit.collider.GetComponent<BaseEnemyAI>();
					goal = limbo;
					//if anyone is listening, let them know an enemy has been selected
					if(onEnemySelected != null) onEnemySelected(player.target);
				}
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//MOVEMENT LOGIC
		//-------------------------------------------------------------------------------------------------------------------
		if (!goal.Equals (limbo) || player.target != null) {
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

			foreach(BaseEnemyAI enemy in player.enemiesInRange){
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
