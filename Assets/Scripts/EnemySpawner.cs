using UnityEngine;
using System.Collections;

namespace EnemySpawning{
	public class EnemySpawner : MonoBehaviour {
		void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, 1);
		}
	}
}