/*******************************************
* Description
* This script is responsible for character's label.
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label{

    // ------ Public Variables ------
    public int id;
    public string label;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public Label(int id){
        this.id = id;
        this.label = "Default Label";
    }

    // ------ Private Functions ------

}
