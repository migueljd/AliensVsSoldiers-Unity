using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class will be used to control every mission's logic, the constructor will receive a mission type(which is an enum in this class).
/// Depending on the mission type, this class will have a specific object type that will be used to validate if the mission has been completed or not.
/// Use the method UpdateMission with it's given type to update the mission status, the return bool of this method let's you know if the mission was
/// completed.
/// 
/// type
/// </summary>

public class MissionController {
	//---------------------------------------------------------------------------------------------------------------------------------------
	//Variables regarding Assault type missions
	//---------------------------------------------------------------------------------------------------------------------------------------
	private List<BaseEnemyAI> enemiesList;



	public enum MissionType {
		Assault = 0,
	}
	
	public MissionType missionType;

	public MissionController(MissionType missionID){
		if (missionID == MissionType.Assault) {
			enemiesList = new List<BaseEnemyAI>();
			GameObject[] enemiesGos = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject go in enemiesGos){
				BaseEnemyAI enemy = go.GetComponent<BaseEnemyAI>();

				if(enemy != null) 
					enemiesList.Add(enemy);

			}
		}

		missionType = missionID;
	}


	//---------------------------------------------------------------------------------------------------------------------------------------
	//Methods regarding Assault type missions
	//---------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Updates the mission completeness according to the enemy, note that this only works for when the mission is of type Assault.
	/// </summary>
	/// <returns><c>true</c>, if mission was finished, <c>false</c> otherwise(or if it's not a killing enemy dependable mission).</returns>
	public bool UpdateMission(BaseEnemyAI deadEnemy){
		if (missionType != MissionType.Assault || deadEnemy == null)
			return false;

		if (enemiesList.Contains (deadEnemy)) {
			enemiesList.Remove(deadEnemy);
		}

		//All enemies are dead, mission accomplished
		if (enemiesList.Count == 0) 
			return true;

		return false;

	}
}
