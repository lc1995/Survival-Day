using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefendAction : Action {

	// ------ Public Variables ------
	[System.Serializable]
	public struct Result{
		public string description;
		public float probability;
		public float atkFactor;
		public float bounceFactor;

		public override string ToString(){
			return string.Format("{0} -- Pro:{1} -- Dmg:{2} -- Bounce:{3}", description, probability, atkFactor, bounceFactor);
		}
	}

	// ------ Shared Variables ------
	public List<Result> results;
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------
	public DefendAction(int id, string description, ActionType type) : base(id, description, type){
		results = new List<Result>();
	}

	public float GetTotalResultProb(){
		float totalProb = 0f;
		foreach(Result r in results){
			totalProb += r.probability;
		}
		return totalProb;
	}

	public void AddResult(string description, float p, float dF){
		Result result = new Result();
		result.description = description;
		result.probability = p;
		result.atkFactor = dF;
		results.Add(result);
	}

	public override string ToString(){
		string str = string.Format("{0}\n", base.ToString());
		int index = 0;
		foreach(Result r in results){
			index += 1;
			str += index + " -- " + r.ToString() + "\n";
		}

		return str;
	}
}
