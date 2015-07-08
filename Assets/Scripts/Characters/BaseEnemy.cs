using UnityEngine;
using System.Collections;

public class BaseEnemy : BaseCharacter {

	[HideInInspector]
	protected BasePlayer target;

	protected bool targetInRange;

	private float destinationUpdateTime;
	

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (target != null) {
			Vector3 targetPosition = target.transform.position;

			MoveTowardsTarget(targetPosition);

			Vector3 lookAtVector = new Vector3 (targetPosition.x, this.transform.position.y, targetPosition.z);

			this.transform.LookAt(lookAtVector);
		}

	}

	protected virtual void MoveTowardsTarget(Vector3 position){
		agent.SetDestination (position);
	}

	public void PlayerEnteredRange(BasePlayer player){
		targetInRange = true;
	}
	
	public void PlayerLeftRange(){
		targetInRange = false;
	}
	
}
