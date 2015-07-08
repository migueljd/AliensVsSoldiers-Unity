using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePlayer : BaseCharacter {



	[HideInInspector]
	public List<BaseEnemy> enemiesInRange;
	[HideInInspector]
	public BaseEnemy target;
	[HideInInspector]
	public BaseEnemy nextEnemyToAttack;




	// Use this for initialization
	protected override void Start () {
		base.Start();
		enemiesInRange = new List<BaseEnemy> ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public virtual void MoveTowards(Vector3 position){
		agent.SetDestination (position);
	}

	public void EnemyEnteredRange(BaseEnemy enemy){
		if(!enemiesInRange.Contains(enemy))enemiesInRange.Add (enemy);
	}

	public void EnemyLeftRange(BaseEnemy enemy){
		enemiesInRange.Remove (enemy);
	}

	void OnTriggerEnter(Collider other){


		if (other.tag == "Enemy") {
			enemiesInRange.Add (other.GetComponent<BaseEnemy>());
			
		}
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Enemy") {
			enemiesInRange.Remove(other.GetComponent<BaseEnemy>());
		}
	}

	public void TargetDied(BaseCharacter character){

		enemiesInRange.Remove((BaseEnemy) character);
		if(enemiesInRange.Count ==0)
		Debug.Log ("Debugging count when enemy died " + enemiesInRange.Count);

	}
	


}
