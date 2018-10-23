/*******************************************
* Description
* This class is the interaction for obtain object
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObtainObject : Interaction {

    // ------ Public Variables ------
    public Inventory inventory;
    public int number;
    public SmallMapObject mapObject;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public InteractionObtainObject(string desc, Inventory inventory, int number, SmallMapObject mapObject) : base(desc){
        this.inventory = inventory;
        this.number = number;
        this.mapObject = mapObject;
    }

    public override void Interact(){
        base.Interact();

        Data.player.ObtainInventory(inventory, number);
        mapObject.DestroyItself();

        UIManager.instance.AddInfoInBoard("你获得了" + inventory.name + " x " + number);
    }

    // ------ Private Functions ------

}
