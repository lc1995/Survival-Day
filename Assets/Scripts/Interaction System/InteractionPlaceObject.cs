﻿/*******************************************
* Description
* This class is the interaction for placing object
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlaceObject : Interaction{

    // ------ Public Variables ------
    public GameObject objectToPlace;
    public float duration;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public InteractionPlaceObject(string desc, GameObject pObject) : base(desc){
        this.objectToPlace = pObject;
        this.duration = 1f;
    }

    public override void Interact(){
        base.Interact();

        PlayerControl.instance.PlaceObject(this);
    }

    // ------ Private Functions ------

}
