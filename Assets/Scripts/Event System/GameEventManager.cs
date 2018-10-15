/*******************************************
* Description
* GameEventManger will manage all game events in the scene
* Battle can be also considered as a special event (it's encapsulated in event)
* GameEvent Manager is a singleton which provides a global accessor
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventManager : MonoBehaviour{

    public static GameEventManager instance = null;

    // ------ Public Variables ------
    

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private PBEvent currentEvent;
    private UIManager uim;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

    void Start(){
        uim = UIManager.instance;
        
    }

    // ------ Public Functions ------
    /// <summary>
    /// Start an event
    /// </summary>
    /// <param name="pbe">PBEvent</param>
    public void StartEvent(PBEvent pbe){
        Time.timeScale = 0f;

        currentEvent = pbe;

        uim.ShowEventBoard();
        uim.SetEventInfo("");
        UpdateUI();
    }

    /// <summary>
    /// End the battle
    /// This function can be only called by BattleManager
    /// </summary>
    public void EndBattle(){
        PBBattleState state = currentEvent.current as PBBattleState;
        switch(BattleManager.instance.ending){
            case BattleEnding.Character1Win:
                currentEvent.SelectAction(state.successAction);
                UpdateUI();
                break;
            case BattleEnding.Character2Win:
                currentEvent.SelectAction(state.failAction);
                UpdateUI();
                break;
        }
    }

    // ------ Private Functions ------

    private void OnSelect(int index=0){
        StartCoroutine(MoveNextState((PBEventAction)currentEvent.current.actions[index]));
    }

    private void EndEvent(){
        Time.timeScale = 1f;

        currentEvent.current.Exit();

        uim.ShowEventBoard(false);     
    }

    private IEnumerator MoveNextState(PBEventAction action){
        // Select action
        currentEvent.SelectAction(action);

        // Here only normal state will update ui
        // Battle state has its own ui behaviors
        if(currentEvent.current.GetType() != typeof(PBBattleState))
            UpdateUI();

        // Check battle state
        // If it's a battle state, start a coroutine for battle
        // Then if will wait unitl battle end and check the result
        while(currentEvent.current.GetType() == typeof(PBBattleState)){
            yield return StartCoroutine(BattleManager.instance.Battle(Data.player, currentEvent.owner));

            PBBattleState state = currentEvent.current as PBBattleState;
            switch(BattleManager.instance.ending){
                case BattleEnding.Character1Win:
                    yield return StartCoroutine(MoveNextState(state.successAction));
                    break;
                case BattleEnding.Character2Win:
                    yield return StartCoroutine(MoveNextState(state.failAction));
                    break;
            }
        }

        // Check final state
        // If it's a final state, there will be only one button which allows player to exit the event
        if(currentEvent.current.actions.Count == 0){
            uim.SetEventBtnText(0, "离开");
            uim.RemoveEventBtnListeners(0);
            uim.AddEventBtnListener(0, (delegate{ EndEvent(); }));
            for(int i = 1; i < uim.GetEventBtnsNumber(); i += 1){
                uim.SetEventBtnText(i, "");
                uim.RemoveEventBtnListeners(i);
            }
        }
    }

    private void UpdateUI(){
        PBEventState currentState = (PBEventState)currentEvent.current;
        uim.SetEventInfo(currentState.description + "\n", true);
        List<PBAction> actions = currentEvent.current.actions;
        int i = 0;
        for(i = 0; i < currentEvent.current.actions.Count; i += 1){
            uim.SetEventBtnText(i, ((PBEventAction)actions[i]).description);
            uim.RemoveEventBtnListeners(i);
            
            int index = i;
            uim.AddEventBtnListener(i, (delegate{ OnSelect(index); }));
        }
        for(; i < uim.GetEventBtnsNumber(); i += 1){
            uim.SetEventBtnText(i, "");
            uim.RemoveEventBtnListeners(i);
        }
    }
}
