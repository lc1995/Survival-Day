/*******************************************
* Description
* This class is responsible for all accessories.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Accessory : Inventory {

    // ------ Public Variables ------
    public float pDefense;
    public float mDefense;
    public float strength;
    public float intellect;
    public float agility;
    public float health;
    public float coldWarm;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Accessory(int id) : base(id){
        this.pDefense = 10f;
        this.mDefense = 10f;
        this.strength = 5f;
        this.intellect = 5f;
        this.agility = 5f;
        this.health = 20f;
        this.coldWarm = 20f;
    }

    // ------ Private Functions ------

}
