using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleEnding{
	idle,
	Character1Win,
	Character2Win,
	Character1Escape,
	Character2Escape
}

public enum BattleState{
	Idle,
	InGame,
	GameOver
}

public class BattleManager : MonoBehaviour {

	public static BattleManager instance = null;

	// ------ Public Variables ------
	public Character player;
	public Character enemy;
	public BattleState state;
	public BattleEnding ending;

	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	private int turns = 0;
	private int atkIndex = -1;
	private int defIndex = -1;
	private Character winner;
	private UIManager uim;
	
	// ------ Required Components ------

	// ------ Event Functions ------
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start(){
		uim = UIManager.instance;
	}
	
	// ------ Public Functions ------
	public void TestCase(){
		player = new Character("你", true);
		enemy = new Character("赌博机");

		StartCoroutine(Battle(player, enemy));
	}

	public void StartBattle(Character c1, Character c2){
		StartCoroutine(Battle(c1, c2));
	}

	// ------ Private Functions ------
	public IEnumerator Battle(Character c1, Character c2){

		// Initialization
		turns = 0;
		state = BattleState.InGame;
		ending = BattleEnding.idle;
		for(int i = 0; i < uim.GetEventBtnsNumber(); i += 1){
			uim.RemoveEventBtnListeners(i);
			int index = i;
			uim.AddEventBtnListener(i, delegate { OnSelect(index); });
		}
		uim.SetEventInfo("战斗开始\n");
		uim.AddEventDividingLine();

		// Game main iteration
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

		// Game end
		if(winner == c1)
			ending = BattleEnding.Character1Win;
		else if(winner == c2)
			ending = BattleEnding.Character2Win;
		uim.SetEventInfo("获胜者 : " + winner.name, true);	
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
			int i = 0;
			for(; i < attackChoices.Count; i += 1){
				uim.RemoveEventBtnListeners(i);
				int index = i;	
				uim.AddEventBtnListener(i, delegate { OnSelect(index); });
				uim.SetEventBtnText(i, attackChoices[i].description.ToBattleString(attacker, defender));
			}
			for(; i < uim.GetEventBtnsNumber(); i += 1){
				uim.RemoveEventBtnListeners(i);
			}
			uim.SetEventInfo("你想要如何进攻：\n", true);

			atkIndex = -1;
			yield return new WaitWhile(() => atkIndex == -1);
		}else{
			atkIndex = Random.Range(0, attackChoices.Count);
		}

		// Get attack choice and show text
		AttackAction atkChoice = attackChoices[atkIndex];
		uim.SetEventInfo(attacker.name + atkChoice.description.ToBattleString(attacker, defender) + "\n", true);

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
			int i = 0;
			for(; i < defendChoices.Count; i += 1){
				uim.RemoveEventBtnListeners(i);	
				int index = i;		
				uim.AddEventBtnListener(i, delegate { OnSelect(index); });
				uim.SetEventBtnText(i, defendChoices[i].description.ToBattleString(attacker, defender));
			}
			for(; i < uim.GetEventBtnsNumber(); i += 1){
				uim.RemoveEventBtnListeners(i);
			}
			uim.SetEventInfo("你想要如何防御：\n", true);

			defIndex = -1;
			yield return new WaitWhile(() => defIndex == -1);
		}else{
			defIndex = Random.Range(0, defendChoices.Count);
		}

		// Get defend choice and show text
		DefendAction defChoice = defendChoices[defIndex];
		uim.SetEventInfo(defChoice.description.ToBattleString(attacker, defender) + "\n", true);

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
		uim.SetEventInfo(result.description.ToBattleString(attacker, defender) + "\n", true);
		// Show enemy's hp (only in demo)
		string hpStr = defender.currentProperty.hp.ToString() + " / " + defender.originalProperty.hp.ToString();
		uim.SetEventInfo(defender.name + "的血量 : " + hpStr + "\n", true);
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
