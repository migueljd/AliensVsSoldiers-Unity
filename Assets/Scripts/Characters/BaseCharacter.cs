﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (NavMeshAgent))]
public class BaseCharacter : MonoBehaviour {

	public delegate void CharacterDied (BaseCharacter character);
	public CharacterDied onCharacterDiedE;

	protected NavMeshAgent agent;

	public int hp;

	public int damage;

	void OnEnable(){
		onCharacterDiedE += GameController.CharacterDied;
	}

	void OnDisable(){
		onCharacterDiedE -= GameController.CharacterDied;
	}

	// Use this for initialization
	protected virtual void Start () {
		this.agent = this.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	}

	public virtual void DealDamage(BaseCharacter character){
		character.TakeDamage (damage);
		if (this.tag == "Enemy") {
			Debug.Log (hp);
		}
	}

	public virtual void TakeDamage(int damage){
		this.hp = this.hp - damage;

		if (hp <= 0)
			Dead ();
//		Debug.Log ("I, " + this.name + " took " + damage + " and now I have " + hp);

	}

	public void Dead(){
		if(onCharacterDiedE != null) onCharacterDiedE (this);
		Destroy (this.gameObject);

	}

	public virtual void AttackFrame(){
		
	}

	public virtual void EndAttackFrame(){
	
	}

	public virtual void StartAttackAnimation(){
	}
}
