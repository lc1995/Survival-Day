/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Result{

    // ------ Public Variables ------
    public int id;
    public string description;

    public float atkFactor;
    public float ctkFactor;

    public float param;
    public float actParam;
    public float weapParam;
    public float aglParam;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Result(int id){
        this.id = id;
    }

    // ------ Private Functions ------

}
