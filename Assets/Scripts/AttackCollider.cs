﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackCollider : MonoBehaviour {

	[HideInInspector]
	public bool isAttacking;

	[HideInInspector]
	public int attackDamage;

	[Tooltip("Is this an enemy attack collider?")]
	public bool isEnemyAttackCollider;



	void OnTriggerEnter(Collider other){
		Debug.Log ("Collision occurred");
		if (isAttacking) {
			if (isEnemyAttackCollider && other.tag == "Player") {
				DealDamageToCharacter (other.GetComponent<BasePlayer> ());
			} else if (!isEnemyAttackCollider && other.tag == "Enemy") {
				DealDamageToCharacter (other.GetComponent<BaseEnemy> ());
			}
		}
	}


	void DealDamageToCharacter(BaseCharacter character){
		character.TakeDamage (attackDamage);
	}
}
