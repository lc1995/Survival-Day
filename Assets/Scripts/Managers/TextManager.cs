using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
	public static TextManager instance = null;
	public Text txt;

	public static string globalTextInfo;

	void Awake(){
		if(instance==null)
			instance = this;
		else if(instance!=this)
			Destroy(gameObject);

		// DontDestroyOnLoad(gameObject);
		InitTextfield();
	}

	void InitTextfield(){
		// txt.text="";
		globalTextInfo="";
		txt.text=globalTextInfo;
	}

	public void UpdateTextField(string s){
		globalTextInfo=s+globalTextInfo;
		txt.text=globalTextInfo;
	}

	public string ReturnText(){
		return globalTextInfo;
	}
}
