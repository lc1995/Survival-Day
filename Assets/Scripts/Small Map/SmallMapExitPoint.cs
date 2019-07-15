/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapExitPoint : MonoBehaviour {

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    // ------ Public Functions ------

    // ------ Private Functions ------

}
