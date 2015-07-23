using UnityEngine;
using System.Collections;

public class RangedPlayer : BasePlayer {

	public GameObject shotPrefab;
	public Transform weaponShootingSocket;
	
	//------------------------------------------------------------------------------------------------------------------------------
	//ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------
	public override void AttackFrame(){
		Shoot ();
	}
	
	public void Shoot(){
	

		Projectile proj = ((GameObject)Instantiate (shotPrefab, weaponShootingSocket.position, Quaternion.LookRotation (this.transform.forward))).GetComponent<Projectile>();
		proj.damage = this.damage;
		proj.enemyProjectile = false;
	}
	
	public void AnimationEventAttack(){
		AttackFrame ();
	}

	public override void StartAttackAnimation ()
	{
		base.StartAttackAnimation ();
		this.transform.LookAt (nextEnemyToAttack.transform.position);
	}
	
	//------------------------------------------------------------------------------------------------------------------------------
	//END OF ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------

}
