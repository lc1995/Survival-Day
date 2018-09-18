using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	public Button[] buttons;

	private delegate void del(int i);

	// Use this for initialization
	void Start () {
		A b1 = new B();
		B b2 = new B();
		A b3 = b2;

		Debug.Log("b1 : " + b1.GetType().ToString());
		Debug.Log("b2 : " + b2.GetType().ToString());
		Debug.Log("b3 : " + b2.GetType().ToString());
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

public class A{
	
}

public class B : A{

}
