/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigMapManager : MonoBehaviour {

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
        Vector2 touchPos;
        #if UNITY_STANDALONE
        if(!Input.GetMouseButtonDown(0))
            return;
        touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        #elif UNITY_IOS
        if(Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began)
            return;
        touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #endif

        foreach(Collider2D c in Physics2D.OverlapPointAll(touchPos)){
            if(c.isTrigger && c.GetComponent<BigMapObject>()){
                EnterSmallMap(c.GetComponent<BigMapObject>().sceneID);
            }
        }        
	}

    // ------ Public Functions ------

    // ------ Private Functions ------
    private void EnterSmallMap(int sceneID){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
    }
}
