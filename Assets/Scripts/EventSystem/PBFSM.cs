/*******************************************
* Description
* Probability-based Finite State Machine
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PBJob();

public class PBFSM{
    
    // Current state
    public PBState current;
    private PBState starting;

    public PBFSM(PBState state){
        starting = state;
        current = state;
    }

    public void Reset(){
        current = starting;
    }

    public List<PBAction> GetCurrentActions(){
        return current.actions;
    }

    public void SelectAction(int actionIndex){
        current.Exit();
        current = current.actions[actionIndex].NextState();
        current.Enter();
    }

    public void SelectAction(PBEventAction action){
        current.Exit();
        current = action.NextState();
        current.Enter();
    }
}

public class PBState{

    public List<PBAction> actions;

    public PBJob[] enterJobs = new PBJob[0];
    public PBJob[] exitJobs = new PBJob[0];

    public PBState(){
        actions = new List<PBAction>();
    }

    public void AddAction(params PBAction[] list){
        foreach(PBAction a in list){
            actions.Add(a);
        }
    }

    public void Enter(){
        foreach(PBJob j in enterJobs){
            j();
        }
    }

    public void Exit(){
        foreach(PBJob j in exitJobs){
            j();
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
