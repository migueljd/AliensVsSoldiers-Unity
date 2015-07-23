﻿using UnityEngine;
using System.Collections;

public class RangedEnemyFSM : BaseEnemyAI {

	public GameObject shotPrefab;
	public Transform weaponShootingSocket;
	
	//------------------------------------------------------------------------------------------------------------------------------
	//ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------
	public override void AttackFrame(){
		Shoot ();
	}
	
	public void Shoot(){
		
		((GameObject)Instantiate (shotPrefab, weaponShootingSocket.position, Quaternion.LookRotation (this.transform.forward))).GetComponent<Projectile>().damage = this.damage;
	}

	public void AnimationEventAttack(){
		AttackFrame ();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	//END OF ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------


	//---------------------------------------------------------------------------------------------------------------------------------------------------
	//SPECIFIC FSM FOR THE RANGED ENEMY
	//---------------------------------------------------------------------------------------------------------------------------------------------------
	
	//---------------------------------------------------------------------------------------------------------------------------------------------------
	//END OF SPECIFIC FSM FOR THE RANGED ENEMY
	//---------------------------------------------------------------------------------------------------------------------------------------------------

}
