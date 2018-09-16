/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EventState{
    Idle,
    Start,
    Stay,
    End
}

public class GameEventManager : MonoBehaviour{

    public static GameEventManager instance = null;

    // ------ Public Variables ------
    public Canvas eventCanvas;
    public Text text;
	public ScrollRect sr;
	public Button[] buttons;
    public Text infoText;
    public PBEvent currentEvent;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private int selectIndex = 0;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

    void Start(){
        // TestCase2();
    }

    // ------ Public Functions ------
    public void StartEvent(Character enemy, PBEvent pbe){
        currentEvent = pbe;
        StartCoroutine(IEStartEvent(enemy, pbe));
    }

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

    public void ResetEvent(){
        BattleManager.instance.StopAllCoroutines();
        // TestCase2();
    }

    // ------ Private Functions ------
    private IEnumerator IEStartEvent(Character enemy, PBEvent pbe){
        infoText.gameObject.SetActive(true);
        infoText.text = pbe.name;
        yield return new WaitForSeconds(1f);
        infoText.gameObject.SetActive(false);

        eventCanvas.gameObject.SetActive(true);
        UpdateUI();

        yield return new WaitUntil(() => CheckFinalState());

        yield return new WaitForSeconds(1f);
        eventCanvas.gameObject.SetActive(false);
    }

    private bool CheckFinalState(){
        if(currentEvent.current.actions.Count == 0)
            return true;
        else
            return false;
    }

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

    private void TestCase2(){
        PBEventState s1 = new PBEventState("遇到一个赌博机。");
        PBEventState s2 = new PBEventState("看起来这台老虎机还能用。");
        PBEventState s3 = new PBEventState("你离开了。");
        PBEventState s4 = new PBEventState("获得资源。");
        PBEventState s5 = new PBEventState("颗粒无收。");
        PBEventState s6 = new PBEventState("获得大笔金钱。");
        PBEventState s7 = new PBEventState("中了超级大乐透。");
        PBBattleState s8 = new PBBattleState("赌博机活了，并向你冲了过来");
        s8.enterJobs = new PBJob[] { BattleEnter };
        PBEventState s9 = new PBEventState("你摧毁了赌博机，获得了里面所有奖品");
        PBEventState s10 = new PBEventState("你被赌博机吃了");

        PBEventAction a1 = new PBEventAction("检查一下");
        a1.AddTransition(s2, 1);
        PBEventAction a2 = new PBEventAction("远离黄赌毒");
        a2.AddTransition(s3, 1);
        PBEventAction a3 = new PBEventAction("溜了溜了");
        a3.AddTransition(s3, 1);
        PBEventAction a4 = new PBEventAction("感觉手气爆表，有点膨胀");
        a4.AddTransition(s4, 0.3f);
        a4.AddTransition(s5, 0.3f);
        a4.AddTransition(s6, 0.3f);
        a4.AddTransition(s7, 0.1f);
        PBEventAction a5 = new PBEventAction("...");
        a5.AddTransition(s2, 1);
        PBEventAction a6 = new PBEventAction("踹一脚");
        a6.AddTransition(s8, 1);
        PBEventAction a7 = new PBEventAction("...");
        PBEventAction a8 = new PBEventAction("...");
        a7.AddTransition(s9, 0);
        a8.AddTransition(s10, 0);

        s1.AddAction(a1, a2);
        s2.AddAction(a3, a4, a6);
        s4.AddAction(a5);
        s5.AddAction(a5);
        s6.AddAction(a5);
        s7.AddAction(a5);
        s8.AddAction(a7, a8);

        currentEvent = new PBEvent(s1, "赌博机事件");

        UpdateUI();
    }

    private void BattleEnter(){
        BattleManager.instance.TestCase();
    }

    private void BattleExit(){

    }

    private void OnSelect(int index=0){
        selectIndex = index;

        currentEvent.SelectAction(selectIndex);

        UpdateUI();
    }

    private void UpdateUI(){
        PBEventState currentState = (PBEventState)currentEvent.current;
        text.text = currentState.description;
        List<PBAction> actions = currentEvent.current.actions;
        int i = 0;
        for(i = 0; i < currentEvent.current.actions.Count; i += 1){
            buttons[i].GetComponentInChildren<Text>().text = ((PBEventAction)actions[i]).description;
            buttons[i].onClick.RemoveAllListeners();
            
            int index = new int();
            index = i;
            buttons[i].onClick.AddListener(delegate{ OnSelect(index); });
        }
        for(; i < buttons.Length; i += 1){
            buttons[i].GetComponentInChildren<Text>().text = "";
            buttons[i].onClick.RemoveAllListeners();
        }
    }
}
