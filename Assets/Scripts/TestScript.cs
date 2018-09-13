using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	private const string testStr = "$1 攻击了 $2";

	// Use this for initialization
	void Start () {
		StartCoroutine(YYY());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator FFF(){
		Debug.Log("1");
		yield return new WaitForSeconds(2);
		
		Debug.Log("2");
	}

	private IEnumerator YYY(){

		yield return StartCoroutine(FFF());

		Debug.Log("3");

	}
}
