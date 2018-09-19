/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallMapObjectAI{

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public abstract Vector2 GetDirection(Transform obj, Transform player);

    // ------ Private Functions ------

}
