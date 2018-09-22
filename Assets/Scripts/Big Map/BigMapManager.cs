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
		Data.inBigMap = true;
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
        touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        #elif UNITY_WEBGL
        if(Input.GetMouseButtonDown(0))
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else if(Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        else
            return;
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
