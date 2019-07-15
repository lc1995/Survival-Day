/*******************************************
* Description
* This class is the base class of interaction
* Interaction has the following type:
* 1. Event
* 2. Place Object
* 3. Obtain Object
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction{

    // ------ Public Variables ------
    public string description;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Interaction(string desc){
        description = desc;
    }

    public virtual void Interact(){
        UIManager.instance.OnJoystickClick(false);
    }

    // ------ Private Functions ------

}
