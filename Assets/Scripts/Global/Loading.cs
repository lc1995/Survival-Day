using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// This class is used to load all necessary data before game logic starts
/// By now it should be executed first --> Set in Script Execution Order
/// </summary>
public class Loading : MonoBehaviour {

	// ------ Public Variables ------
	public Text loadingText;
	
	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	private const string defAttacksFileName = "Attacks.json";
	private const string defDefendsFileName = "Defends.json";
	private const string defResultsFileName = "Results.json";
	private const string defEquipmentsFileName = "Equipments.json";
	private const string defMaterialsFileName = "Materials.json";
	private const string defFoodsFileName = "Foods.json";
	
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
		loadingText.text = "读取攻击数据...";
		lft.fileName = defAttacksFileName;
		yield return StartCoroutine(LoadFile(lft));

		AttacksData attacksData = JsonUtility.FromJson<AttacksData>(lft.fileText);

		foreach(Attack atk in attacksData.attacks){
			Data.AllAttacks.Add(atk.id, atk);
		}

		// Load defends data
		loadingText.text = "读取防御数据...";
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
		loadingText.text = "读取攻击结果数据...";
		lft.fileName = defResultsFileName;
		yield return StartCoroutine(LoadFile(lft));

		ResultsData resultsData = JsonUtility.FromJson<ResultsData>(lft.fileText);

		foreach(Result res in resultsData.results){
			Data.AllResults.Add(res.id, res);
		}

		// Load equipments data
		loadingText.text = "读取装备数据...";
		lft.fileName = defEquipmentsFileName;
		yield return StartCoroutine(LoadFile(lft));

		EquipmentsData equipmentsData = JsonUtility.FromJson<EquipmentsData>(lft.fileText);
		
		foreach(Weapon weapon in equipmentsData.weapons){
			Sprite sprite = Resources.Load<Sprite>("Sprites/Weapons/" + weapon.id);
			weapon.sprite = sprite;

			Data.AllWeapons.Add(weapon.id, weapon);
			Data.AllInventories.Add(weapon.id, weapon as Inventory);
		}
		foreach(Armor armor in equipmentsData.armors){
			Sprite sprite = Resources.Load<Sprite>("Sprites/Clothes/" + armor.id);
			armor.sprite = sprite;

			Data.AllArmors.Add(armor.id, armor);
			Data.AllInventories.Add(armor.id, armor as Inventory);
		}
		foreach(Accessory accessory in equipmentsData.accessories){
			Sprite sprite = Resources.Load<Sprite>("Sprites/Hats/" + accessory.id);
			accessory.sprite = sprite;

			Data.AllAccessories.Add(accessory.id, accessory);
			Data.AllInventories.Add(accessory.id, accessory as Inventory);
		}

		// Load materials data
		loadingText.text = "读取材料数据...";
		lft.fileName = defFoodsFileName;
		yield return StartCoroutine(LoadFile(lft));

		FoodsData foodsData = JsonUtility.FromJson<FoodsData>(lft.fileText);

		foreach(Food food in foodsData.foods){
			Data.AllFoods.Add(food.id, food);
			Data.AllInventories.Add(food.id, food as Inventory);
		}

		// Load foods data
		loadingText.text = "读取食物数据...";
		lft.fileName = defMaterialsFileName;
		yield return StartCoroutine(LoadFile(lft));

		MaterialsData materialsData = JsonUtility.FromJson<MaterialsData>(lft.fileText);

		foreach(Material material in materialsData.materials){
			Data.AllMaterials.Add(material.id, material);
			Data.AllInventories.Add(material.id, material as Inventory);
		}

		// Test: Check loading is right
		Debug.Log("Total Attacks Loaded : " + Data.AllAttacks.Count);
		Debug.Log("Total Defends Loaded : " + Data.AllDefends.Count);
		Debug.Log("Total Results Loaded : " + Data.AllResults.Count);
		Debug.Log("Total Inventories Loaded : " + Data.AllInventories.Count);
		Debug.Log("Total Weapons Loaded : " + Data.AllWeapons.Count);
		Debug.Log("Total Armors Loaded : " + Data.AllArmors.Count);
		Debug.Log("Total Accessories Loaded : " + Data.AllAccessories.Count);
		Debug.Log("Total Materials Loaded : " + Data.AllMaterials.Count);
		Debug.Log("Total Foods loaded : " + Data.AllFoods.Count);

		// Test: Start small map
		UnityEngine.SceneManagement.SceneManager.LoadScene(3);
	}

	private IEnumerator LoadFile(LoadFileText lft){
		string filePath = Path.Combine(Application.streamingAssetsPath, lft.fileName);

		if(filePath.Contains("://") || filePath.Contains(":///")){
			// On some specific platform, StreamingAssets cannot be directly accessed
			UnityWebRequest www = new UnityWebRequest(filePath);
			yield return www;
			lft.fileText = www.downloadHandler.text;
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

[System.Serializable]
public struct EquipmentsData{
	public List<Weapon> weapons;
	public List<Armor> armors;
	public List<Accessory> accessories;
}

[System.Serializable]
public struct MaterialsData{
	public List<Material> materials;
}

[System.Serializable]
public struct FoodsData{
	public List<Food> foods;
}
