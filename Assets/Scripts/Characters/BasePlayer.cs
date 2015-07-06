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

	public void StartAttack(){
		
	}

	void OnTriggerEnter(Collider other){


		if (other.tag == "Enemy") {
			enemiesInRange.Add (other.GetComponent<BaseEnemy>());
			
		}
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Enemy") {
			enemiesInRange.Remove(other.GetComponent<BaseEnemy>());
		}
	}

	public void StartAttackAnimation(){
		if (nextEnemyToAttack != null)
			this.transform.LookAt (nextEnemyToAttack.transform.position);
		//this is a placeholder, the warrior animation should be other
		this.GetComponent<WarriorAnimationDemo>().animator.SetTrigger("Attack1Trigger");
	}
	


}
