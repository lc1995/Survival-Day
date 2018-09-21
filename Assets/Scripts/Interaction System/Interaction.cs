/*******************************************
* Description
*
*
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
