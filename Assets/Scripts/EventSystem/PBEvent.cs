/*******************************************
* Description
* Probability-based Event (Inherited from PBFSM)
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBEvent : PBFSM {

    public string name;
    public Character owner;

    public PBEvent(PBEventState state, string n, Character eventOwner) : base(state){
        name = n;
        owner = eventOwner;
    }
}

public class PBEventState : PBState{

    public static PBEventState defaultState = new PBEventState();

    public string description;
    public bool isFinal;

    public PBEventState(string d="", bool final=false) : base(){
        description = d;
        isFinal = final;
    }
}

public class PBBattleState : PBEventState{

    public PBEventAction successAction;
    public PBEventAction failAction;

    public PBBattleState() : base(){

    }

    public void AddAction(PBEventAction success, PBEventAction fail){
        successAction = success;
        failAction = fail;
        actions.Add(success);
        actions.Add(fail);
    }
}

public class PBEventAction : PBAction{

    public string description;
    public bool isHidden;   // The action is hidden or not

    public PBEventAction(string d="") : base(){
        description = d;
    }

    public void AddTransition(PBEventState s, float p){
        pLinks.Add(new PBEventTransition(s, p));
    }
}

public class PBEventTransition : PBTransition{

    public PBEventTransition(PBState s, float p) : base(s, p){
        
    }
}
