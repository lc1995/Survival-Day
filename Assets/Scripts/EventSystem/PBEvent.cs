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

    public PBEvent(PBEventState state, string n) : base(state){
        name = n;
    }
}

public class PBEventState : PBState{

    public string description;

    public PBEventState(string d) : base(){
        description = d;
    }

}

public class PBEventAction : PBAction{

    public string description;

    public PBEventAction(string d) : base(){
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
