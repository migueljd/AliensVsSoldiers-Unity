using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public delegate void OnCharacterDied(BaseCharacter character);
	public static OnCharacterDied onCharacterDiedE;

	private static GameController instance;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void CharacterDied(BaseCharacter character){
		if (onCharacterDiedE != null) 
			onCharacterDiedE (character);
	}
}
