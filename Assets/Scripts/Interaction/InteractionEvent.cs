﻿/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : Interaction{

    // ------ Public Variables ------
    public PBEvent pBEvent;
    
    // ------ Shared Variables ------
    
    
    // ------ Private Variables ------
    
    
    // ------ Required Components ------
    
    
    // ------ Public Functions ------
    public InteractionEvent(string desc, PBEvent pbe, bool active) : base(desc, active){
        pBEvent = pbe;
    }

    public override void Interact(){
        base.Interact();

        GameEventManager.instance.StartEvent(pBEvent);
    }
    
    // ------ Private Functions ------
    
    
}