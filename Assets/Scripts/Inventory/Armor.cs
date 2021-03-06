﻿/*******************************************
* Description
* This class is responsible for all armors.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Armor : Inventory {

    // ------ Static Variables ------
    public static Armor Default = new Armor(-1);

    // ------ Public Variables ------
    public float pDefense;
    public float mDefense;
    public float strength;
    public float intellect;
    public float agility;
    public float health;
    public float coldWarm;
    public int[] effect;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Armor(int id) : base(id){
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
