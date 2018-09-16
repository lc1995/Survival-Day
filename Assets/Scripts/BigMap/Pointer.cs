using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	void Start () {
		
	}
	
	void Update(){
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit){
				string objName = hit.collider.gameObject.name;
				if (objName.Contains("Point")) {
					int sceneNumber = Int32.Parse(objName.Substring(5));
					Debug.Log(sceneNumber);

					switch (sceneNumber)
					{
						case 1:
							Application.LoadLevel(1);
							break;
						case 2:
							Application.LoadLevel(2);
							break;
						default:
							break;
					}
				}
			}
		}


	}
}
