using UnityEngine;
using System.Collections;

public class AlienPlayer : BasePlayer {

	public AttackCollider attackCollider;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		attackCollider.attackDamage = this.damage;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	//--------------------------------------------------------------------------------------------------------------
	//ANIMATION RELATED CODE
	//--------------------------------------------------------------------------------------------------------------

	public void AnimationEventAttackFrame(){
		AttackFrame ();
	}

	public void AnimationEventEndAttackFrame(){
		EndAttackFrame ();
	}
	
	public override void AttackFrame(){
		base.AttackFrame ();
		attackCollider.isAttacking = true;
	}

	public override void EndAttackFrame(){
		base.EndAttackFrame ();
		attackCollider.isAttacking = false;
	}

	//--------------------------------------------------------------------------------------------------------------
	//END OF ANIMATION RELATED CODE
	//--------------------------------------------------------------------------------------------------------------
}
