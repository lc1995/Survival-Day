/*******************************************
* Description
* This class is responsible for all weapons. It includes:
* 1. Fist
* 2. Melee
* 3. Magic
* 4. Ranged
* 5. Special
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
    Fist,
    Melee,
    Magic,
    Ranged,
    Special
}

[System.Serializable]
public class Weapon : Inventory {

    // ------ Public Variables ------
    public WeaponType type;
    public float pAtk;
    public float mAtk;
    public float accuracy;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public Weapon(int id, WeaponType type) : base(id){
        this.type = type;

        this.pAtk = 10f;
        this.mAtk = 10f;
        this.accuracy = 100f;
    }

    // ------ Private Functions ------

}
