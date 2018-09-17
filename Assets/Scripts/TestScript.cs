using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	public Button[] buttons;

	private delegate void del(int i);

	// Use this for initialization
	void Start () {
		for(int i=0; i < 3; i++){
			int index = i;
			buttons[i].onClick.AddListener(delegate { OnSelect(index); });
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnSelect(int index=0){
		Debug.Log(index);
	}

	public void TTT(){
		Debug.Log("It works.");
	}
}
