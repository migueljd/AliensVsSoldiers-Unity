using UnityEngine;
using System.Collections;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public abstract class BaseEnemyAI : BaseCharacter {

	public float detectionRange;
	public float attackRange;

	public Transform mesh;

	private Vector3 initialPosition;

	private FSMSystem fsm;

	private GameObject player;

	protected override void Start(){
		base.Start ();


		this.initialPosition = this.transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");

		MakeFSM ();
	}

	protected override void Update(){
		base.Update ();
		fsm.CurrentState.Reason(player, this.gameObject);
		fsm.CurrentState.Act(player, this.gameObject);
	}

	//----------------------------------------------------------------------------------------------------------------------------------
	//FSM SYSTEM FUNCTIONS
	//----------------------------------------------------------------------------------------------------------------------------------

	public void SetTransition(Transition t){
		fsm.PerformTransition (t);
	}

	// The NPC has 3 states: Idle and ChasingPlayer 
	// If it's on the first state and SawPlayer transition is fired, it changes to ChasePlayer
	// If it's on ChasePlayerState and LostPlayer transition is fired, it returns to FollowPath
	protected void MakeFSM()
	{
		fsm = new FSMSystem ();

		IdleState idleState = new IdleState (detectionRange, initialPosition);
		idleState.AddTransition (Transition.PlayerDetected, StateID.ChasingPlayer);

		ChasingPlayerState chasingState = new ChasingPlayerState (this.detectionRange, this.attackRange);
		chasingState.AddTransition (Transition.PlayerConcealed, StateID.Idle);

		fsm.AddState (idleState);
		fsm.AddState (chasingState); 
	}
	//----------------------------------------------------------------------------------------------------------------------------------
	//END OF FSM SYSTEM FUNCTIONS
	//----------------------------------------------------------------------------------------------------------------------------------
	

	//------------------------------------------------------------------------------------------------------------------------------
	//ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// This function is called by the Act state whenever the unit is supposed to attack, implement it according to how the unit is supposed
	/// to do so.
	/// </summary>
	public virtual void Attack(){
		StartAttackAnimation ();
	}

	/// <summary>
	/// All units must have an attack animation, this will be called to ensure that happens
	/// </summary>
	public override void StartAttackAnimation(){
		GetComponent<Animator> ().SetTrigger ("Attack1Trigger");
	}

	//------------------------------------------------------------------------------------------------------------------------------
	//END OF ATTACK FUNCTIONS
	//------------------------------------------------------------------------------------------------------------------------------



	#if UNITY_EDITOR 
	//This method helps checking if the enemy is attacking/detecting correctly
	void OnDrawGizmos(){
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(this.transform.position, Vector3.up, detectionRange);

		Handles.color = Color.red;
		Handles.DrawWireDisc(this.transform.position, Vector3.up, attackRange);
	}
	
	void OnDrawGizmosSelected(){
		Handles.color = Color.Lerp(Color.yellow, Color.green, 0.8f);
		Handles.DrawWireDisc(this.transform.position, Vector3.up, detectionRange);
		
		Handles.color = Color.Lerp(Color.red, Color.green, 0.8f);
		Handles.DrawWireDisc(this.transform.position, Vector3.up, attackRange);
	}
	#endif

}

//---------------------------------------------------------------------------------------------------------------------------------------------------
//FSM for the basic enemy
//---------------------------------------------------------------------------------------------------------------------------------------------------

public class IdleState : FSMState{
	

	private float detectionRange;
	private Vector3 initialPosition;

	public IdleState(float detectionRange, Vector3 initialPosition){
		stateID = StateID.Idle;

		this.detectionRange = detectionRange;
		this.initialPosition = initialPosition;

	}
	
	public override void Reason (GameObject player, GameObject npc)
	{
		if (player != null) {
			if (Vector3.Distance (player.transform.position, npc.transform.position) <= detectionRange) {
				npc.GetComponent<BaseEnemyAI> ().SetTransition (Transition.PlayerDetected);
			}
		}
	}

	public override void Act (GameObject player, GameObject npc)
	{
		if (player != null) {
			if (Vector3.Distance (npc.transform.position, this.initialPosition) > 0.1) {
				npc.GetComponent<NavMeshAgent> ().SetDestination (this.initialPosition);
			}
		}

	}
	
}

public class ChasingPlayerState : FSMState{
	private float detectionRange;
	private float attackRange;

	public ChasingPlayerState(float detectionRange, float attackRange){
		stateID = StateID.ChasingPlayer;

		this.detectionRange = detectionRange;
		this.attackRange = attackRange;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		if (player != null) {
			float distanceToPLayer = Vector3.Distance (player.transform.position, npc.transform.position);
			if (distanceToPLayer > detectionRange) {
				npc.GetComponent<BaseEnemyAI> ().SetTransition (Transition.PlayerConcealed);
			} 
		}
	}

	public override void Act (GameObject player, GameObject npc)
	{
		if (player != null) {
			float distanceToPLayer = Vector3.Distance (player.transform.position, npc.transform.position);
			if (distanceToPLayer > attackRange) {
			
				//Go to a point between your current position and the player position that would put you in attack range
				Vector3 aiGoal = (player.transform.position - npc.transform.position).normalized * attackRange + player.transform.position;
				npc.GetComponent<BaseEnemyAI> ().MoveTowards (aiGoal);
			} else {
				npc.transform.LookAt (player.gameObject.transform);
				npc.GetComponent<BaseEnemyAI> ().Attack ();
			}
		}
	}


}

//---------------------------------------------------------------------------------------------------------------------------------------------------
//End of FSM for the basic enemy
//---------------------------------------------------------------------------------------------------------------------------------------------------

