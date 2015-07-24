using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public Text enemyKillCount;

	public RectTransform healthBar;

	public Text finalText;
	public RectTransform finalPanel;

	public Text healthCount;

	private int killCount;

	private Vector3 maxHealthPosition;
	private Vector3 minHealthPosition;
	private int playerHealth;
	private int playerMaxHealth;

	private BasePlayer player;

	void OnEnable(){
		GameController.onCharacterDiedE += OnCharacterDied;
		GameController.onGameOverE += UpdateFinalPanel;
	}

	void OnDisable(){
		GameController.onCharacterDiedE -= OnCharacterDied;
		GameController.onGameOverE -= UpdateFinalPanel;

		if(player != null) player.onPlayerTookDamageE -= OnPlayerTookDamage;
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ();
		player.onPlayerTookDamageE += OnPlayerTookDamage;

		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<BasePlayer> ().hp;
		playerMaxHealth = playerHealth;
		maxHealthPosition = healthBar.transform.localPosition;
		minHealthPosition = new Vector3(maxHealthPosition.x - healthBar.rect.width, maxHealthPosition.y, maxHealthPosition.z);
		healthCount.text = string.Format ("{0}/{1}", playerHealth, playerMaxHealth);


	}

	private void OnCharacterDied(BaseCharacter character){
		if (character is BaseEnemyAI) {
			killCount++;
			enemyKillCount.text = "Enemies killed: " + killCount;
		} 
	}

	public void UpdateFinalPanel(bool victory){
		if (victory) {
			finalText.text = "Victory achieved!";
			finalText.color = Color.yellow;
		}
		else{
			finalText.text = "Mission failed!";
			finalText.color = Color.red;
		}

		finalPanel.gameObject.SetActive (true);
	}

	//--------------------------------------------------------------------------------------------------------------------------------
	//HEALTH BAR RELATED
	//--------------------------------------------------------------------------------------------------------------------------------

	private void OnPlayerTookDamage(int damage){
		playerHealth = playerHealth - damage < 0? 0 : (playerHealth - damage > playerMaxHealth? playerMaxHealth : playerHealth - damage);

		healthBar.localPosition = Vector3.Lerp(minHealthPosition, maxHealthPosition, (float)playerHealth/(float)playerMaxHealth);
		healthCount.text = string.Format ("{0}/{1}", playerHealth, playerMaxHealth);
	}
}
