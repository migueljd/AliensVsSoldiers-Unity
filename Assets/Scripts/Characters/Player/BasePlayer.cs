using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePlayer : BaseCharacter {



	[HideInInspector]
	public List<BaseEnemyAI> enemiesInRange;
	[HideInInspector]
	public BaseEnemyAI target;
	[HideInInspector]
	public BaseEnemyAI nextEnemyToAttack;

	public GameObject meshGameObject;

	public delegate void PlayerTookDamage(int damage);
	public PlayerTookDamage onPlayerTookDamageE;

	protected override void OnEnable(){
		base.OnEnable ();
		GameController.onCharacterDiedE += EnemyDied;
	}
	
	protected override void OnDisable(){
		base.OnEnable ();
		GameController.onCharacterDiedE -= EnemyDied;
	}


	// Use this for initialization
	protected override void Start () {
		base.Start();
		enemiesInRange = new List<BaseEnemyAI> ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
	

	public void EnemyEnteredRange(BaseEnemyAI enemy){
		if(!enemiesInRange.Contains(enemy))enemiesInRange.Add (enemy);
	}

	public void EnemyLeftRange(BaseEnemyAI enemy){
		enemiesInRange.Remove (enemy);
	}

	public void EnemyDied(BaseCharacter character){
		if ((character is BaseEnemyAI) && enemiesInRange.Contains ((BaseEnemyAI)character)) {
			enemiesInRange.Remove ((BaseEnemyAI)character);
		}

	}

	public override void StartAttackAnimation(){
		meshGameObject.GetComponent<Animator> ().SetTrigger ("Attack1Trigger");
	}

	public override void TakeDamage(int damage){
		base.TakeDamage (damage);
		if (onPlayerTookDamageE != null)
			onPlayerTookDamageE (damage);
	}
	


}
