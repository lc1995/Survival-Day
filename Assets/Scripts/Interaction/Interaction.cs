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
    public bool isActive;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Interaction(string desc, bool active){
        description = desc;
        isActive = active;
    }

    public virtual void Interact(){
        UIManager.instance.OnJoystickClick(false);
    }

    // ------ Private Functions ------

}
