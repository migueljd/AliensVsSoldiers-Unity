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

	public GameObject meshGameObject;

	public delegate void PlayerTookDamage(int damage);
	public PlayerTookDamage onPlayerTookDamageE;

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
	

	public void EnemyEnteredRange(BaseEnemy enemy){
		if(!enemiesInRange.Contains(enemy))enemiesInRange.Add (enemy);
	}

	public void EnemyLeftRange(BaseEnemy enemy){
		enemiesInRange.Remove (enemy);
	}

	public void EnemyDied(BaseCharacter character){
		if ((character is BaseEnemy) && enemiesInRange.Contains ((BaseEnemy)character)) {
			enemiesInRange.Remove ((BaseEnemy)character);
		}

	}

	public override void StartAttackAnimation(){
		meshGameObject.GetComponent<WarriorAnimationDemo> ().animator.SetTrigger ("Attack1Trigger");
	}

	public override void TakeDamage(int damage){
		base.TakeDamage (damage);
		if (onPlayerTookDamageE != null)
			onPlayerTookDamageE (damage);
	}
	


}
