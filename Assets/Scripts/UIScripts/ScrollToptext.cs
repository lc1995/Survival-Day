using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToptext : MonoBehaviour {

	private Scrollbar _scrollbar;

	void OnEnable(){
		// Debug.Log("SCROLL ENABLED");
        _scrollbar = GetComponent<Scrollbar>() as Scrollbar;
		_scrollbar.value=1;
    }
}
