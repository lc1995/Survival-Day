using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data{

	public static Character player;
	public static bool inBigMap;

	public static Dictionary<int, Attack> AllAttacks = new Dictionary<int, Attack>();
	public static Dictionary<int, Defend> AllDefends = new Dictionary<int, Defend>();
	public static Dictionary<int, Result> AllResults = new Dictionary<int, Result>();
	public static Dictionary<ActionType, List<int>> AllDefendsByType = new Dictionary<ActionType, List<int>>();

	public static Dictionary<int, Inventory> AllInventories = new Dictionary<int, Inventory>();
	public static Dictionary<int, Weapon> AllWeapons = new Dictionary<int, Weapon>();
	public static Dictionary<int, Armor> AllArmors = new Dictionary<int, Armor>();
	public static Dictionary<int, Accessory> AllAccessories = new Dictionary<int, Accessory>();
	public static Dictionary<int, Item> AllItems = new Dictionary<int, Item>();
	public static Dictionary<int, Food> AllFoods = new Dictionary<int, Food>();
	public static Dictionary<int, Material> AllMaterials = new Dictionary<int, Material>();
	
}
