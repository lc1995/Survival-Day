using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToptext : MonoBehaviour {

	public GameObject sceneText;
	public GameObject currentCanvas;

	public Text txtFull;
	public TextManager textManager;

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>() as Button;
		button.onClick.AddListener(btClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void btClick(){
		txtFull.text=textManager.ReturnText();
		sceneText.SetActive(true);
		currentCanvas.SetActive(false);
		Time.timeScale=0;
	}
}
