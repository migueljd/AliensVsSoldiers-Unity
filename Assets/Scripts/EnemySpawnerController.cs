using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnemySpawning{

	public class EnemySpawnerController : MonoBehaviour {


		[Tooltip("The max amount of enemies alive in a given time")]
		public int enemiesAliveLimit;

		[Tooltip("How many enemies should be spawned before the game ends")]
		public int enemiesToBeSpawned;

		public float timeBetweenSpawns;

		private float timeForNextSpawn;

		private GameObject[] possibleEnemies;

		private bool keepSpawing;

		private static EnemySpawnerController instance;

		private int currentEnemyCount = 0;

		private List<EnemySpawner> spawners;

		void Awake(){
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy (this.gameObject);
		}

		// Use this for initialization
		void Start () {
			possibleEnemies = (GameObject[]) Resources.LoadAll<GameObject> (Variables.enemiesPrefabPath);
			Debug.Log (Application.dataPath + Variables.enemiesPrefabPath);

			spawners = new List<EnemySpawner> ();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("EnemySpawner")){
				spawners.Add(go.GetComponent<EnemySpawner>());
			}

			Debug.Log (string.Format("Enemies count: {0}", possibleEnemies.Length));
			Debug.Log (string.Format("Enemies spawners count: {0}", spawners.Count));

		}

		void Update(){
			if(currentEnemyCount < enemiesAliveLimit && Time.time >= timeForNextSpawn){
				timeForNextSpawn = Time.time + timeBetweenSpawns;
				Spawn ();
			}
		}

		private void Spawn(){
			int chosenSpawner = Random.Range (0, spawners.Count);
			int chosenEnemy = Random.Range (0, possibleEnemies.Length);

			GameObject newEnemy=(GameObject)Instantiate (possibleEnemies [chosenEnemy], spawners [chosenSpawner].transform.position, Quaternion.identity);

			newEnemy.GetComponent<BaseEnemy> ().onCharacterDiedE += OnCharacterDied;

			currentEnemyCount += 1;
		}

		private void OnCharacterDied(BaseCharacter character){
			currentEnemyCount -= 1;
		}

	}
}
