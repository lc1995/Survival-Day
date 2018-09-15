/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvent : MonoBehaviour{

    // ------ Public Variables ------
    public Text text;
	public ScrollRect sr;
	public Button[] buttons;
    public PBEvent currentEvent;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private int selectIndex = 0;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start(){
        TestCase();


    }

    // ------ Public Functions ------

    // ------ Private Functions ------
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

    private void OnSelect(int index=0){
        selectIndex = index;
        Debug.Log(index);

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
