/*******************************************
* Description
* This class is the interaction for placing object
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlaceObject : Interaction{

    // ------ Public Variables ------
    public GameObject objectToPlace;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Public Functions ------
    public InteractionPlaceObject(string desc, GameObject pObject) : base(desc){
        objectToPlace = pObject;
    }

    public override void Interact(){
        PlayerControl.instance.PlaceObject(objectToPlace);
    }

    // ------ Private Functions ------

}
