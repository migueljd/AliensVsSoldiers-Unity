using UnityEngine;
using System.Collections;

public class PlayerFollowingCamera : MonoBehaviour {

	private Transform player;

	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		lastPosition = player.position;
	}

	void Update(){
		if (player.position != lastPosition) {
			Vector3 currentDiff = player.position - lastPosition;

			float newX = this.transform.position.x + currentDiff.x;
			float newY = this.transform.position.y;
			float newZ = this.transform.position.z + currentDiff.z;

			Vector3 cameraFinalVector = new Vector3(newX, newY, newZ);

			this.transform.position = cameraFinalVector;

			lastPosition = player.position;
		}
	}
	

}
