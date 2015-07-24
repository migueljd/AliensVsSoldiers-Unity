using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public Text enemyKillCount;

	public RectTransform healthBar;

	private int killCount;

	private Vector3 maxHealthPosition;
	private Vector3 minHealthPosition;
	private int playerHealth;
	private int playerMaxHealth;

	void OnEnable(){
		GameController.onCharacterDiedE += OnCharacterDied;
	}

	void OnDisable(){
		GameController.onCharacterDiedE -= OnCharacterDied;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().onPlayerTookDamageE -= OnPlayerTookDamage;
	}

	void Start(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().onPlayerTookDamageE += OnPlayerTookDamage;

		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().hp;
		playerMaxHealth = playerHealth;
		maxHealthPosition = healthBar.transform.localPosition;
		minHealthPosition = new Vector3(maxHealthPosition.x - healthBar.rect.width, maxHealthPosition.y, maxHealthPosition.z);

//		healthBar.position = new Vector3(136.7f, 1094.0f, 0.0f);

		Debug.Log (maxHealthPosition);
		Debug.Log (minHealthPosition);
		Debug.Log (healthBar.localScale);
	}

	private void OnCharacterDied(BaseCharacter character){
		if (character is BaseEnemyAI) {
			killCount++;
			enemyKillCount.text = "Enemies killed: " + killCount;
		}
	}

	private void OnPlayerTookDamage(int damage){
		playerHealth = playerHealth - damage < 0? 0 : (playerHealth - damage > playerMaxHealth? playerMaxHealth : playerHealth - damage);

		healthBar.localPosition = Vector3.Lerp(minHealthPosition, maxHealthPosition, (float)playerHealth/(float)playerMaxHealth);
		Debug.Log ((float)playerHealth / (float)playerMaxHealth);
		Debug.Log ("Lerp result would be(local): " + healthBar.localPosition);
		Debug.Log ("Lerp result would be(global): " + healthBar.position);
	}
}
