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
	private const string defAttacksFileName = "Attacks.json";
	private const string defDefendsFileName = "Defends.json";
	private const string defResultsFileName = "Results.json";
	
	// ------ Required Components ------
	
	void Awake(){
		InitGameData();
	}

	private void InitGameData(){
		StartCoroutine(LoadData());
	}

	private IEnumerator LoadData(){
		LoadFileText lft = new LoadFileText();

		// Load attacks data
		lft.fileName = defAttacksFileName;
		yield return StartCoroutine(LoadFile(lft));

		AttacksData attacksData = JsonUtility.FromJson<AttacksData>(lft.fileText);

		foreach(Attack atk in attacksData.attacks){
			Data.AllAttacks.Add(atk.id, atk);
		}

		// Load defends data
		lft.fileName = defDefendsFileName;
		yield return StartCoroutine(LoadFile(lft));

		DefendsData defendsData = JsonUtility.FromJson<DefendsData>(lft.fileText);

		foreach(ActionType type in System.Enum.GetValues(typeof(ActionType))){
			Data.AllDefendsByType.Add(type, new List<int>());
		}
		foreach(Defend def in defendsData.defends){
			Data.AllDefends.Add(def.id, def);
			Data.AllDefendsByType[def.type].Add(def.id);
		}

		// Load results data
		lft.fileName = defResultsFileName;
		yield return StartCoroutine(LoadFile(lft));

		ResultsData resultsData = JsonUtility.FromJson<ResultsData>(lft.fileText);

		foreach(Result res in resultsData.results){
			Data.AllResults.Add(res.id, res);
		}

		// Test: Check loading is right
		Debug.Log(Data.AllAttacks.Count);
		Debug.Log(Data.AllDefends.Count);
		Debug.Log(Data.AllResults.Count);
		Debug.Log(Data.AllAttacks[1001].types.Count);

		// Test: Start small map
		UnityEngine.SceneManagement.SceneManager.LoadScene(3);
	}

	private IEnumerator LoadFile(LoadFileText lft){
		string filePath = Path.Combine(Application.streamingAssetsPath, lft.fileName);

		if(filePath.Contains("://") || filePath.Contains(":///")){
			// On some specific platform, StreamingAssets cannot be directly accessed
			WWW www = new WWW(filePath);
			yield return www;
			lft.fileText = www.text;
		}else{
			lft.fileText = File.ReadAllText(filePath);
		}
	}
}

class LoadFileText{
	public string fileName;
	public string fileText;
}

[System.Serializable]
public struct AttacksData{
	public List<Attack> attacks;
}

[System.Serializable]
public struct DefendsData{
	public List<Defend> defends;
}

[System.Serializable]
public struct ResultsData{
	public List<Result> results;
}
