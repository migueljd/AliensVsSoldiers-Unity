using UnityEngine;
using System.Collections;

public class AlienPlayer : BasePlayer {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void AttackFrame(){
		if(nextEnemyToAttack != null)base.DealDamage (nextEnemyToAttack);
	}
}
