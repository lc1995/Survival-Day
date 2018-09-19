/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAI : SmallMapObjectAI {

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------  

    // ------ Public Functions ------
    public override Vector2 GetDirection(Transform obj, Transform player){
        return Vector2.zero;
    }

    // ------ Private Functions ------

}
