using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType{
	Fist = 0,	// 拳脚
	MeleeWeapon = 1,	// 近战武器
	RangedWeapon = 2,	// 远程武器
	Magic = 3,	// 魔法
	Special = 4,	// 特殊
}

public class Action {

	// ------ Public Variables ------
	public int id;
	public string description;
	public ActionType type;

	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------
	public Action() {}

	public Action(int id, string description, ActionType type){
		this.id = id;
		this.description = description;
		this.type = type;
	}

	public override string ToString(){
		return string.Format("{0} -- {1} -- {2}", id, description, type);
	}
}
