/*******************************************
* Description
* Probability-based Finite State Machine
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBFSM{
    
    // Current state
    public PBState current;

    public PBFSM(PBState state){
        current = state;
    }

    public List<PBAction> GetCurrentActions(){
        return current.actions;
    }

    public void SelectAction(int actionIndex){
        current = current.actions[actionIndex].NextState();
    }
}

public class PBState{

    public List<PBAction> actions;

    public PBState(){
        actions = new List<PBAction>();
    }

    public void AddAction(params PBAction[] list){
        foreach(PBAction a in list){
            actions.Add(a);
        }
    }
}

public class PBAction{

    public List<PBTransition> pLinks;

    public PBAction(){
        pLinks = new List<PBTransition>();
    }

    public void AddTransition(PBTransition t){
        pLinks.Add(t);
    }

    public void AddTransition(PBState s, float p){
        pLinks.Add(new PBTransition(s, p));
    }

    /// <summary>
    /// Calculate next action based on probability
    /// </summary>
    public PBState NextState(){
        // Calculate total probability
        float total = 0f;
        foreach(PBTransition t in pLinks){
            total += t.probability;
        }

        // Calculate next action
        float rand = Random.Range(0f, total);
        foreach(PBTransition t in pLinks){
            rand -= t.probability;
            if(rand <= 0f)
                return t.nextState;
        }

        Debug.LogError("It's impossible that no available state is calculated!\n");
        return null;
    }
}

public class PBTransition{

    public PBState nextState;
    public float probability;

    public PBTransition(PBState s, float p){
        nextState = s;
        probability = p;
    }
}
