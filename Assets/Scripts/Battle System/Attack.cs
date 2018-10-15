using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType{
	Fist = 0,	// 拳脚
	MeleeWeapon = 1,	// 近战武器
	Magic = 2,	// 远程武器
	RangedWeapon = 3,	// 魔法
	Special = 4,	// 特殊
}

[System.Serializable]
public class Attack{

	// ------ Public Variables ------
	public int id;
	
	public string choice;
	public string description;
	public string objGenerated;

	public List<ActionType> types;
	public float pDamage;
	public float mDamage;

	public float strength;
	public float aligity;
	public float intellect;

	public float precision;
	public int condition;
	public int cd;
	public int ammo;

	
	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------
	public Attack(int id){
		types = new List<ActionType>();
		
		this.id = id;
	}

	public override string ToString(){
		return string.Format("{0} : {1}", id, description);
	}
}
