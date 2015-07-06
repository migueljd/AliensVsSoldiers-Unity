using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage;

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

	void OnCollisionEnter(Collision collision){

		if (collision.collider.tag == "Player" && enemyProjectile) {
			collision.collider.GetComponent<BaseCharacter>().TakeDamage(this.damage);
			Destroy(this.gameObject);
		} 
		else if (collision.collider.tag == "Enemy" && !enemyProjectile) {
			collision.collider.GetComponent<BaseCharacter>().TakeDamage(this.damage);
			Destroy(this.gameObject);
		}
	}

}
