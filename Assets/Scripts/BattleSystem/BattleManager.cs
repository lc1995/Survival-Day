using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleEnding{
		Character1Win,
		Character2Win,
		Character1Escape,
		Character2Escape
}

public class BattleManager : MonoBehaviour {

	public static BattleManager instance = null;

	enum BattleState{
		Idle,
		InGame,
		GameOver
	}

	// ------ Public Variables ------
	public Text text;
	public ScrollRect sr;
	public Button btn1;
	public Button btn2;
	public Button btn3;

	public Character player;
	public Character enemy;
	public BattleEnding ending;

	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	private BattleState state = BattleState.Idle;
	private int turns = 0;
	private int atkIndex = -1;
	private int defIndex = -1;
	private Character winner;
	
	// ------ Required Components ------

	// ------ Event Functions ------
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start(){
		// TestCase();
	}
	
	// ------ Public Functions ------
	public void TestCase(){
		player = new Character("你", true);
		enemy = new Character("赌博机");

		StartCoroutine(StartBattle(player, enemy));
	}

	// ------ Private Functions ------
	private IEnumerator StartBattle(Character c1, Character c2){
		// Wait 0.1 seconds
		yield return new WaitForSeconds(1f);

		// Initialization
		turns = 0;
		btn1.onClick.AddListener(delegate { OnSelect(0); });
		btn2.onClick.AddListener(delegate { OnSelect(1); });
		btn3.onClick.AddListener(delegate { OnSelect(2); });
		text.AddText("战斗开始！！！！！\n", sr);

		while(true){
			turns += 1;

			/// Player turn
			yield return StartCoroutine(Turn(c1, c2));
			// Check whether the battle is over
			CheckBattleIsOver(c1, c2);
			if(state == BattleState.GameOver)
				break;

			/// Enemy turn
			yield return StartCoroutine(Turn(c2, c1));
			// Check whether the battle is over
			CheckBattleIsOver(c1, c2);
			if(state == BattleState.GameOver)
				break;
		}

		if(winner == c1)
			ending = BattleEnding.Character1Win;
		else if(winner == c2)
			ending = BattleEnding.Character2Win;
		text.AddText("获胜者 : " + winner.name, sr);

		yield return new WaitForSeconds(1);

		GameEventManager.instance.EndBattle();
	}

	private IEnumerator Turn(Character attacker, Character defender){

		// Initialization
		state = BattleState.InGame;
		atkIndex = -1;
		defIndex = -1;

		// Updata attacker's pool and generate attack choices randomly
		UpdatePool(attacker);
		List<AttackAction> attackChoices = GenerateAttackChoices(attacker);

		// - If the attacker is player
		// 1. Enable buttons and update their texts
		// 2. Block until player selects
		// 3. After player selects, disable buttons
		// - Else
		// 1. Defender randomly selects one attack
		if(attacker.isPlayer){
			btn1.GetComponentInChildren<Text>().text = attackChoices[0].description.ToBattleString(attacker, defender);
			btn1.interactable = true;
			btn2.GetComponentInChildren<Text>().text = attackChoices[1].description.ToBattleString(attacker, defender);
			btn2.interactable = true;
			btn3.GetComponentInChildren<Text>().text = attackChoices[2].description.ToBattleString(attacker, defender);
			btn3.interactable = true;
			text.AddText("你想要如何进攻：\n", sr);

			atkIndex = -1;
			yield return new WaitWhile(() => atkIndex == -1);

			// Disable attack choices buttons
			btn1.interactable = false;
			btn2.interactable = false;
			btn3.interactable = false;
		}else{
			atkIndex = Random.Range(0, attackChoices.Count);
		}

		// Get attack choice and show text
		AttackAction atkChoice = attackChoices[atkIndex];
		text.AddText(attacker.name + atkChoice.description.ToBattleString(attacker, defender) + "\n", sr);

		// Updata defender's pool and generate defend choices randomly
		UpdatePool(defender);
		List<DefendAction> defendChoices = GenerateDefendChoices(defender, atkChoice.type);
		
		// - If the attacker is player
		// 1. Enable buttons and update their texts
		// 2. Block until player selects
		// 3. After player selects, disable buttons
		// - Else
		// 1. Defender randomly selects one attack
		if(defender.isPlayer){
			btn1.GetComponentInChildren<Text>().text = defendChoices[0].description.ToBattleString(attacker, defender);
			btn1.interactable = true;
			btn2.GetComponentInChildren<Text>().text = defendChoices[1].description.ToBattleString(attacker, defender);
			btn2.interactable = true;
			btn3.GetComponentInChildren<Text>().text = defendChoices[2].description.ToBattleString(attacker, defender);
			btn3.interactable = true;
			text.AddText("你想要如何防御：\n", sr);

			defIndex = -1;
			yield return new WaitWhile(() => defIndex == -1);

			// Disable attack choices buttons
			btn1.interactable = false;
			btn2.interactable = false;
			btn3.interactable = false;
		}else{
			defIndex = Random.Range(0, defendChoices.Count);
		}

		// Get defend choice and show text
		DefendAction defChoice = defendChoices[defIndex];
		text.AddText(defChoice.description.ToBattleString(attacker, defender) + "\n", sr);

		// Calculate result
		float totalProb = defChoice.GetTotalResultProb();
		DefendAction.Result result = defChoice.results[0];
		foreach(DefendAction.Result r in defChoice.results){
			totalProb -= r.probability;
			if(totalProb <= 0f)
				result = r;
		}

		// Process result
		defender.GetDamage(result.atkFactor * atkChoice.baseDamage);

		// Show result's text
		text.AddText(result.description.ToBattleString(attacker, defender) + "\n", sr);
		// Show enemy's hp (only in demo)
		string hpStr = defender.currentProperty.hp.ToString() + " / " + defender.originalProperty.hp.ToString();
		text.AddText(defender.name + "的血量 : " + hpStr + "\n", sr);
	}

	private void UpdatePool(Character ch){
		ch.attackPool = Data.AllAttacks;
		ch.defendPool = Data.AllDefendsByType;
	}

	private List<AttackAction> GenerateAttackChoices(Character ch, int size=3){
		List<AttackAction> actions = new List<AttackAction>();

		AttackAction tmp;
		int range = ch.attackPool.Count;
		for(int i = 0; i < size; i++){
			tmp = ch.attackPool[Random.Range(0, range)];
			while(actions.Contains(tmp))
				tmp = ch.attackPool[Random.Range(0, range)];
			actions.Add(tmp);
		}

		return actions;
	}

	private List<DefendAction> GenerateDefendChoices(Character en, ActionType type, int size=3){
		List<DefendAction> actions = new List<DefendAction>();

		DefendAction tmp;
		int range = en.defendPool.Count;
		for(int i = 0; i < size; i++){
			tmp = en.defendPool[type][Random.Range(0, range)];
			while(actions.Contains(tmp))
				tmp = en.defendPool[type][Random.Range(0, range)];
			actions.Add(tmp);
		}

		return actions;
	}

	private void CheckBattleIsOver(Character ch1, Character ch2){
		winner = null;
		if(ch1.currentProperty.hp <= 0){
			winner = ch2;
			state = BattleState.GameOver;
		}else if(ch2.currentProperty.hp <= 0){
			winner = ch1;
			state = BattleState.GameOver;
		}
	}
	
	private void OnSelect(int index=0){
		atkIndex = index;
		defIndex = index;
	}

}
