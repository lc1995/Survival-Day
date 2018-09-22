using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This class is used to load all necessary data before game logic starts
/// By now it should be executed first --> Set in Script Execution Order
/// </summary>
public class Loading : MonoBehaviour {

	// ------ Public Variables ------
	
	
	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	private const string defActionsFileName = "Actions.json";
	
	// ------ Required Components ------
	
	void Awake(){
		InitGameData();
	}

	private void InitGameData(){
		Data.player = new Character("Player", true);

		StartCoroutine(LoadDefendData());
	}

	private IEnumerator LoadDefendData(){
		string filePath = Path.Combine(Application.streamingAssetsPath, defActionsFileName);
		string actionsText;

		if(filePath.Contains("://") || filePath.Contains(":///")){
			// On some specific platform, StreamingAssets cannot be directly accessed
			WWW www = new WWW(filePath);
			yield return www;
			actionsText = www.text;
		}else{
			actionsText = File.ReadAllText(filePath);
		}

		ActionsData actionsData = JsonUtility.FromJson<ActionsData>(actionsText);

		// Loading actions
		Data.AllDefends = actionsData.defends;
		Data.AllAttacks = actionsData.attacks;
		Debug.Log("Total defends loaded : " + Data.AllDefends.Count);
		Debug.Log("Total attacks loaded : " + Data.AllAttacks.Count);
		
		// Classify defends
		foreach(ActionType at in System.Enum.GetValues(typeof(ActionType)))
			Data.AllDefendsByType[at] = new List<DefendAction>();
		foreach(DefendAction da in Data.AllDefends){
			Data.AllDefendsByType[da.type].Add(da);
		}
	}

}

[System.Serializable]
public struct ActionsData{
	public List<DefendAction> defends;
	public List<AttackAction> attacks;
}
