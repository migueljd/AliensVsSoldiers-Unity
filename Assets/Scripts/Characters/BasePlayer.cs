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


	public override void StartAttackAnimation(){
		if (nextEnemyToAttack != null)
			this.transform.LookAt (nextEnemyToAttack.transform.position);
		//this is a placeholder, the warrior animation should be other
		this.GetComponent<WarriorAnimationDemo>().animator.SetTrigger("Attack1Trigger");
	}

	public void AssignTargetDelegate(){
		if (nextEnemyToAttack != null) {
			nextEnemyToAttack.onCharacterDiedE += TargetDied;
		}
	}

	public void UnassignTargetDelegate(){
		if (nextEnemyToAttack != null) {
			nextEnemyToAttack.onCharacterDiedE -= TargetDied;
		}
	}

	public void TargetDied(BaseCharacter character){

		enemiesInRange.Remove((BaseEnemy) character);
		if(enemiesInRange.Count ==0)
		Debug.Log ("Debugging count when enemy died " + enemiesInRange.Count);
	}

}
