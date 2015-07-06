using UnityEngine;
using System.Collections;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (SphereCollider))]
public class BaseCharacter : MonoBehaviour {

	protected NavMeshAgent agent;

	public float hp;

	public float damage;


	// Use this for initialization
	protected virtual void Start () {
		this.agent = this.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	}

	public virtual void DealDamage(BaseCharacter character){
		character.TakeDamage (damage);
	}

	public virtual void TakeDamage(float damage){
		this.hp = this.hp - damage;

	}

	public virtual void AttackFrame(){
		
	}
}
