using UnityEngine;
using System.Collections;

public class RangedAI : BaseEnemy {

	public GameObject shotPrefab;


	public float bestDistanceFromTarget;
//
//	private float shootSpeed = .5f;
//	private float nextAttackTime;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (shotPrefab == null) {
			shotPrefab = (GameObject) Resources.Load("Prefabs/ShotObjects/Default");
		}


		if (bestDistanceFromTarget == 0)
			bestDistanceFromTarget = 1;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (target != null) {
			if(targetInRange){
				StartAttackAnimation();
			} 
		}
	}

	public override void MoveTowards (Vector3 position)
	{
		if(target != null){
			if(Vector3.Distance(this.transform.position, target.transform.position) <= bestDistanceFromTarget){
				position = -this.transform.forward*bestDistanceFromTarget;
			}
			
			base.MoveTowards (position);
		}
	}

	//------------------------------------------------------------------------------------------------------------------------------
	//ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------
	public void AnimationEventAttack(){
		AttackFrame ();
	}

	public override void AttackFrame(){
		Shoot ();
	}

	public void Shoot(){

		((GameObject)Instantiate (shotPrefab, this.transform.position, Quaternion.LookRotation (this.transform.forward))).GetComponent<Projectile>().damage = this.damage;
	}


	public override void StartAttackAnimation(){
		GetComponent<Animator> ().SetTrigger ("Attack1Trigger");
	}



	

}
