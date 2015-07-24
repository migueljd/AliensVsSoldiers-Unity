using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	/// <summary>
	/// This variable tells if the game is currently running or not, it is false before the games begin and when the game ends(after the mission was
	/// complreted or failed), true otherwise.
	/// </summary>
	public static bool gameRunning = false;

	public delegate void OnCharacterDied(BaseCharacter character);
	/// <summary>
	/// This event is called whenever a character has died
	/// </summary>
	public static OnCharacterDied onCharacterDiedE;

	///<summary>
	/// If the mission os completed, the victory bool is true, false if the mission was failed.
	/// </summary>
	public delegate void OnGameOver(bool victory);
	/// <summary>
	/// This event is called whenever the game is over
	/// </summary>
	public static OnGameOver onGameOverE;


	public GameObject moveSelector;
	public GameObject attackSelector;

	public MissionController.MissionType missionType;

	private static GameController instance;

	private MissionController missionController;

	
	void Awake(){
		if (instance == null) {
			instance = this;
			missionController = new MissionController(missionType);
		}
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
		gameRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This method must be called whenever a character is about to die, in here, whoever needs to be notified of the character's death, will be
	/// </summary>
	/// <param name="character">Character.</param>
	public static void CharacterDied(BaseCharacter character){
		//Put the selector back on the Selector transform if this guy was it's parent
		if (character.transform == instance.attackSelector.transform.parent) {
			instance.attackSelector.GetComponent<Selector>().GoBackToParent();
		}

		Debug.Log (character);
		//Check if the mission was completed
		if (character is BaseEnemyAI && instance.missionType == MissionController.MissionType.Assault) {
			if (instance.missionController.UpdateMission ((BaseEnemyAI)character)) {
				instance.MissionOver (true);
			}
		} 
		//check if the player is the character that died
		else if (character is BasePlayer) {
			gameRunning = false;
			instance.MissionOver(false);
			Debug.Log ("Mission failed");
		}

		//Let, whoever needs to, know that a character has died
		if (onCharacterDiedE != null) 
			onCharacterDiedE (character);
	}

	private void MissionOver(bool victory){
		if (onGameOverE != null)
			onGameOverE (victory);
	}
	
	public void RestartGame(){
		Application.LoadLevel (Application.loadedLevel);
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
