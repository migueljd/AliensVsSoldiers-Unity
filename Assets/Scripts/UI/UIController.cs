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
		GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().onPlayerTookDamageE += OnPlayerTookDamage;
	}

	void OnDisable(){
		GameController.onCharacterDiedE -= OnCharacterDied;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().onPlayerTookDamageE -= OnPlayerTookDamage;
	}

	void Start(){

		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().hp;
		playerMaxHealth = playerHealth;
		maxHealthPosition = healthBar.transform.position;
		minHealthPosition = new Vector3(maxHealthPosition.x - healthBar.rect.width, maxHealthPosition.y, maxHealthPosition.z);
	}

	private void OnCharacterDied(BaseCharacter character){
		if (character is BaseEnemy) {
			killCount++;
			enemyKillCount.text = "Enemies killed: " + killCount;
		}
	}

	private void OnPlayerTookDamage(int damage){
		playerHealth = playerHealth - damage < 0? 0 : (playerHealth - damage > playerMaxHealth? playerMaxHealth : playerHealth - damage);

		healthBar.position = Vector3.Lerp(minHealthPosition, maxHealthPosition, (float)playerHealth/(float)playerMaxHealth);
	}
}
