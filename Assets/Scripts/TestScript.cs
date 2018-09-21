using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	public Button[] buttons;

	private delegate void del(int i);

	// Use this for initialization
	void Start () {
		StartCoroutine(A());
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

	public IEnumerator A(){
		Debug.Log("1");

		yield return StartCoroutine(B());

		Debug.Log("6");
	}

	public IEnumerator B(){
		Debug.Log("2");

		yield return StartCoroutine(C());

		Debug.Log("5");

	}

	public IEnumerator C(){
		Debug.Log("3");

		yield return null;

		Debug.Log("4");
	}
}

