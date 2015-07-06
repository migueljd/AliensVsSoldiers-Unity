using UnityEngine;
using System.Collections;

public class RangedAI : BaseEnemy {

	public GameObject shotPrefab;



	private float shootSpeed = .5f;
	private float nextAttackTime;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (shotPrefab == null) {
			shotPrefab = (GameObject) Resources.Load("ShotObjects/Default");
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (target != null) {
			this.transform.LookAt(target.transform.position + new Vector3(0,.5f,0));
			if(Time.time >= nextAttackTime){
				Shoot ();
				nextAttackTime =Time.time +  shootSpeed;
			}
		}
	}

	public override void AttackFrame(){
		Shoot ();
	}

	public void Shoot(){

		((GameObject)Instantiate (shotPrefab, this.transform.position, Quaternion.LookRotation (this.transform.forward))).GetComponent<Projectile>().damage = this.damage;
	}



	public override void StartAttackAnimation(){
		//Once there is an animation, this will need to be changed
	}
	

}
