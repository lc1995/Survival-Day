using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	public Button[] buttons;

	private delegate void del(int i);

	// Use this for initialization
	void Start () {
		string str = "E3+10|E4-10.1";
		CharProperty cp = CharProperty.standard;
		cp = InequalityParse.EffectParse(str, cp);

		Debug.Log("strength: " + cp.strength);
		Debug.Log("agility: " + cp.agility);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

