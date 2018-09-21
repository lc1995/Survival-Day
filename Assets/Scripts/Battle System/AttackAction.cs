using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAction : Action {

	// ------ Public Variables ------
	public float baseDamage;
	
	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------
	public AttackAction(int id, string description, ActionType type, float bd) : base(id, description, type){
		this.baseDamage = bd;
	}

	public override string ToString(){
		return string.Format("{0}\nBaseDamage : {1}", base.ToString(), baseDamage);
	}
}
