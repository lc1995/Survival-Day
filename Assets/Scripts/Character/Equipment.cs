/*******************************************
* Description
* This class is responsible for all equipments, including weapons and armors
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Weapon,
    Armor,
    Accessory
}

public class Equipment : Inventory {

    // ------ Public Variables ------
    EquipmentType type;
    float pAtk;
    float mAtk;
    float hit;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------

    // ------ Public Functions ------
    public Equipment(int id, EquipmentType type) : base(id){
        this.type = type;
    }

    // ------ Private Functions ------

}
