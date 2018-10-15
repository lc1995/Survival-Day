using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	public Button[] buttons;

	private delegate void del(int i);

	// Use this for initialization
	void Start () {
		string a = "D0\u9762\u5bb9\u6c89\u9759\uff0c\u53cc\u624b\u63e1\u7740D1\u9ad8\u4e3e\u8fc7\u5934\u9876\uff0c\u4e00\u51fb\u65a9\u843d\u3002";
		Debug.Log(a.Replace("D0", "fff"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

