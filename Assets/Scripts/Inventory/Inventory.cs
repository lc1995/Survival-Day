/*******************************************
* Description
* This class is the base class for all inventories.
* Inventory includes equipments(weapon and armor), food, items and others
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    // ------ Public Variables ------
    public int id;
    public string name;
    public string description;
    public float weight;
    public float value;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------

    // ------ Public Functions ------
    public Inventory(int id){
        this.id = id;
        this.name = "Default";
        this.description = "Default";
        this.weight = 1f;
        this.value = 1f;
    }

    // ------ Private Functions ------

}
