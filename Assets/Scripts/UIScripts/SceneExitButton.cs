﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneExitButton : MonoBehaviour {

	public GameObject GeneralUIElements;
	public GameObject currentCanvas;

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>() as Button;
		button.onClick.AddListener(btClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void btClick(){
		GeneralUIElements.SetActive(true);
		currentCanvas.SetActive(false);
		Time.timeScale=1;
	}
}
