/*******************************************
* Description
* Base class of board ui manager.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardBase : MonoBehaviour {

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------

    // ------ Public Functions ------
    public abstract void Enter();
    public abstract void Exit();

    // ------ Private Functions ------

}
