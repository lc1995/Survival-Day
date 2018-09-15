using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defer function to trigger activation condition
// Returns true when transition can fire
public delegate bool FSMCondition();

// Defer function to perform action
public delegate void FSMAction();

// boolean that start the transition


public class FSMTransition {

	// The method to evaluate if the transition is ready to fire
	public FSMCondition Condition;

	// A list of actions to perform when this transition fires
	private FSMAction[] fireActions;

	public FSMTransition(FSMCondition condition, FSMAction[] actions = null) {
		Condition = condition;
		fireActions = actions;
	}

	// Call all  actions
	public void Fire() {
		if(fireActions != null) foreach (FSMAction Action in fireActions) Action();
	}
}

public class FSMState {

	// State name
	public string name;

	// Arrays of actions to perform based on transitions fire (or not)
	public FSMAction[] enterActions = new FSMAction[0];
	public FSMAction[] stayActions = new FSMAction[0];
	public FSMAction[] exitActions = new FSMAction[0];

	// A dictionary of transitions and the states they are leading to
	private Dictionary<FSMTransition, FSMState> links;

	public FSMState() {
		links = new Dictionary<FSMTransition, FSMState>();
	}

	public FSMState(string stateName) : this(){
		name = stateName;
	}

	public void AddTransition(FSMTransition transition, FSMState target) {
		links [transition] = target;
	}

	public FSMTransition VerifyTransitions() {
		foreach (FSMTransition t in links.Keys) {
			if (t.Condition()) return t;
		}
		return null;
	}

	public FSMState NextState(FSMTransition t) {
		return links [t];
	}
	
	// These methods will perform the actions in each list
	public void Enter() { foreach (FSMAction a in enterActions) a(); }
	public void Stay() { foreach (FSMAction a in stayActions) a(); }
	public void Exit() { foreach (FSMAction a in exitActions) a(); }

}

public class FSM {

	// Current state
	public FSMState current;

	public FSM(FSMState state) {
		current = state;
		current.Enter();
	}

	public void ChangeState(FSMState s){
		current.Exit();
		current = s;
		current.Enter();
	}

	// Examine transitions leading out from the current state
	// If a condition is activated, change the current state and
	// take all the actions linked to:
	// 1. Exit from the current state
	// 2. The activated transition
	// 3. Enter to the new state
	// If no transition is activated, take the actions associated
	// to staying in the current state

	public void Update() { // NOTE: this is NOT a MonoBehaviour
		FSMTransition transition = current.VerifyTransitions ();
		if (transition != null) {
			current.Exit();
			transition.Fire();
			current = current.NextState(transition);
			current.Enter();
		} else {
			current.Stay();
		}
	}
}