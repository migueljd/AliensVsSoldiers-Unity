using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public delegate void OnCharacterDied(BaseCharacter character);
	public static OnCharacterDied onCharacterDiedE;

	public GameObject moveSelector;
	public GameObject attackSelector;

	private static GameController instance;
	
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (this.gameObject);
	}

	void OnEnable(){
		PlayerController.onEnemySelected += EnemySelected;
		PlayerController.onGoalSelected += GoalSelected;
	}

	void OnDisable(){
		PlayerController.onEnemySelected -= EnemySelected;
		PlayerController.onGoalSelected -= GoalSelected;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This method must be called whenever a character is about to die, in here, whoever needs to be notified of the character's death, will be
	/// </summary>
	/// <param name="character">Character.</param>
	public static void CharacterDied(BaseCharacter character){
		if (character.transform == instance.attackSelector.transform.parent) {
			instance.attackSelector.GetComponent<Selector>().GoBackToParent();
		}
		if (onCharacterDiedE != null) 
			onCharacterDiedE (character);
	}

	private void MissionComplete(){
		
	}

	//------------------------------------------------------------------------------------------------------------------------------------
	//Listening related methods(see OnEnable and OnDisable method to check which delegate they are related to)
	//------------------------------------------------------------------------------------------------------------------------------------
	private void EnemySelected(BaseEnemyAI enemy){

		Vector3 newPosition = new Vector3(enemy.transform.position.x, attackSelector.transform.position.y, enemy.transform.position.z);

		attackSelector.transform.SetParent (enemy.transform);

		attackSelector.transform.position = newPosition;
	}

	private void GoalSelected(Vector3 goal){

		Vector3 newPosition = new Vector3(goal.x, moveSelector.transform.position.y, goal.z);

		moveSelector.transform.position = newPosition;
	}
}
