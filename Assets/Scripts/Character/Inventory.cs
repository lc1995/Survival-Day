/*******************************************
* Description
* This class is the base class for all inventories.
* Inventory includes equipments(weapon and armor), food, items and others
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{

    // ------ Public Variables ------
    public int id;
    public string name;
    public string description;
    public float weight;

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
    }

    // ------ Private Functions ------

}
