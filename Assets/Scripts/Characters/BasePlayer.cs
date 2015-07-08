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

	void OnEnable(){
		GameController.onCharacterDiedE += EnemyDied;
	}
	
	void OnDisable(){
		GameController.onCharacterDiedE -= EnemyDied;
	}


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

	public void EnemyDied(BaseCharacter character){
		Debug.Log (string.Format("Character Died and it is {0} that character is a BaseEnemy and it is {1} that it's contained in list",
		           (character is BaseEnemy), enemiesInRange.Contains ((BaseEnemy)character)));
		if ((character is BaseEnemy) && enemiesInRange.Contains ((BaseEnemy)character)) {
			enemiesInRange.Remove ((BaseEnemy)character);
			Debug.Log (string.Format ("Enemy died and removed, current count is {0}", enemiesInRange.Count));
		}

	}

	public override void StartAttackAnimation(){
		this.GetComponent<WarriorAnimationDemo> ().animator.SetTrigger ("Attack1Trigger");
	}
	


}
