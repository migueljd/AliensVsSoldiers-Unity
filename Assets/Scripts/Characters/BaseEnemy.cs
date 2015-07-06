using UnityEngine;
using System.Collections;

public class BaseEnemy : BaseCharacter {

	[HideInInspector]
	protected BasePlayer target;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public void PlayerEnteredRange(BasePlayer player){
		Debug.Log ("PlayerEnteredRange");
		target = player;
	}
	
	public void PlayerLeftRange(){
		target = null;
	}
}
