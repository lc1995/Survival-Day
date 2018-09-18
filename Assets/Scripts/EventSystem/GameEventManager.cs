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
    /// <param name="ch">Owner of the event</param>
    /// <param name="pbe">PBEvent</param>
    public void StartEvent(Character ch, PBEvent pbe){
        Time.timeScale = 0f;

        currentEvent = pbe;

        uim.ShowEventBoard();
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

    // Test cases before
    /* 
    private void TestCase(){
        PBEventState s1 = new PBEventState("你在路上看到了一个造型奇特的灯。");
        PBEventState s2 = new PBEventState("一个身材高大的巨人从灯里跑了出来");
        PBEventState s3 = new PBEventState("你的力量充盈持续三天");
        PBEventState s4 = new PBEventState("你获得了5个木材，5块布料和5个矿石");
        PBEventState s5 = new PBEventState("\"好的小虫子，那你离开吧\"神灯消失了");
        PBEventState s6 = new PBEventState("与神灯战斗");
        PBEventState s7 = new PBEventState("获得物品\"神灯\"");
        PBEventState s8 = new PBEventState("灯神将你一脚踢飞，伸个懒腰离开了。");
        PBEventState s9 = new PBEventState("你离开了");

        PBEventAction a1 = new PBEventAction("用手擦一下");
        a1.AddTransition(s2, 1);
        PBEventAction a2 = new PBEventAction("离开");
        a2.AddTransition(s9, 1);
        PBEventAction a3 = new PBEventAction("许愿\"获得神力\"");
        a3.AddTransition(s3, 1);
        PBEventAction a4 = new PBEventAction("许愿\"获得更多资源\"");
        a4.AddTransition(s4, 1);
        PBEventAction a5 = new PBEventAction("...");
        a5.AddTransition(s5, 1);
        PBEventAction a6 = new PBEventAction("...");
        a6.AddTransition(s5, 1);
        PBEventAction a7 = new PBEventAction("许愿\"那就让我再许三个愿望\"");
        a7.AddTransition(s6, 1);
        PBEventAction a8 = new PBEventAction("胜利");
        a8.AddTransition(s7, 1);
        PBEventAction a9 = new PBEventAction("失败");
        a9.AddTransition(s8, 1);

        s1.AddAction(a1, a2);
        s2.AddAction(a3, a4, a7);
        s3.AddAction(a5);
        s4.AddAction(a6);
        s6.AddAction(a8, a9);

        currentEvent = new PBEvent(s1, "灯神事件");

        UpdateUI();
    }
    */

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
                    StartCoroutine(MoveNextState(state.successAction));
                    UpdateUI();
                    break;
                case BattleEnding.Character2Win:
                    StartCoroutine(MoveNextState(state.failAction));
                    UpdateUI();
                    break;
            }
        }

        // Check final state
        // If it's a final state, there will be only one button which allows player to exit the event
        // The PBFSM will run into 
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
        uim.SetEventInfo(currentState.description);
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
