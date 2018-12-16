/*******************************************
* Description
* This script is responsible for character's label.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label{

    // ------ Public Variables ------
    public int id;
    public string name;
    public string description;
    public int visible;
    public int time;
    public string condition;
    public string effect;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Label(int id){
        this.id = id;
        this.name = "Default Label";
        this.description = "Default Description";
        this.visible = 1;
        this.time = -1;
        this.condition = "";
        this.effect = "";
    }

    // ------ Private Functions ------

}
