using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Defend{

	// ------ Public Variables ------
	public int id;
	public string description;
	public ActionType type;

	public int condition;
	public List<int> results;	// IDs of results

	// ------ Shared Variables ------
	
	// ------ Private Variables ------	
	
	// ------ Required Components ------
	
	// ------ Public Functions ------
	public Defend(int id){
		results = new List<int>();

		this.id = id;
	}

	public override string ToString(){
		string str = string.Format("{0}\n", base.ToString());
		int index = 0;
		foreach(int resultID in results){
			index += 1;
			str += index + " -- " + resultID.ToString() + "\n";
		}

		return str;
	}
}
