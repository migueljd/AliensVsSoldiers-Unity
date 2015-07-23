using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	private Transform selectorParent;

	// Use this for initialization
	void Start () {
		selectorParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && this.name == "MoveSelector") {
			GoBackToParent();
		}
	}

	public void GoBackToParent(){
		transform.SetParent(selectorParent);
		transform.localPosition = new Vector3(0,0,0);
	}


}
