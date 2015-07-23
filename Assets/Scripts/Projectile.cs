using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage;

	public float speed = 10;

	public bool enemyProjectile;

	public float lifetime = 2;
	private float deathTime;

	// Use this for initialization
	void Start () {
		deathTime = Time.time + lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= deathTime)
			Destroy (this.gameObject);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && enemyProjectile) {
			other.GetComponent<BaseCharacter> ().TakeDamage (this.damage);
			Destroy (this.gameObject);
		} else if (other.tag == "Enemy" && !enemyProjectile) {
			other.GetComponent<BaseCharacter> ().TakeDamage (this.damage);
			Destroy (this.gameObject);
		} 
		else if (other.tag == "Level") {
			Destroy (this.gameObject);
		}

	}

}
