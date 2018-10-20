/*******************************************
* Description
* This class is repsonsible for all food.
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Inventory {

    // ------ Public Variables ------
    public float hungryRecover;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Food(int id) : base(id){
        this.hungryRecover = 0f;
    }

    // ------ Private Functions ------

}
