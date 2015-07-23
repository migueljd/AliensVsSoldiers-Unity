using UnityEngine;
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

	//It evaluates if the player should go to the given point, if it should, it starts moving and return true, false otherwise
	public virtual void MoveTowards(Vector3 position){
//		if (Vector3.Distance (agent.destination, this.transform.position) <= distanceThresholdToStop && agent.destination != this.transform.position) {
//			agent.updateRotation = false;
//		}
		agent.SetDestination (position);
	}

	
	/// <summary>
	/// Any attack animation should signal when the AttackFrame will be called and how it would work around it. In melee characters this should
	/// be used to signal at which frame the "Can deal damage starting from this frame" happens.
	/// </summary>
	public virtual void AttackFrame(){
		
	}

	/// <summary>
	/// This is mostly for melee characters, used to signal "It won't be dealing damage starting from this frame
	/// </summary>
	public virtual void EndAttackFrame(){
	
	}

	/// <summary>
	/// Every attack should start with an animation, and then, an animation event should call at what point of the animation all the
	/// attacl logic will happen
	/// </summary>
	public virtual void StartAttackAnimation(){
	}
}
