﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for the local map
/// </summary>
public class LocalMap : MonoBehaviour {

	// ------ Public Variables ------
	
	// ------ Shared Variables ------ 
	
	// ------ Private Variables ------
	
	// ------ Required Components ------
	
	// ------ Event Functions ------
	void Update(){
		SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

		foreach(SpriteRenderer sr in renderers){
			sr.sortingOrder = (int)(sr.transform.position.y * -100f);
		}
	}
}
