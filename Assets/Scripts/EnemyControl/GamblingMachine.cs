/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingMachine : MonoBehaviour {

    // ------ Public Variables ------
    public Character gm;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    public PBEvent gmEvent { get; private set; }

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		gm = new Character("Gambling Machine");

        InitEvent();
	}

    void Update () {
		PBEventState s1 = new PBEventState("遇到一个赌博机。");
        PBEventState s2 = new PBEventState("看起来这台老虎机还能用。");
        PBEventState s3 = new PBEventState("你离开了。");
        PBEventState s4 = new PBEventState("获得资源。");
        PBEventState s5 = new PBEventState("颗粒无收。");
        PBEventState s6 = new PBEventState("获得大笔金钱。");
        PBEventState s7 = new PBEventState("中了超级大乐透。");
        PBEventState s8 = new PBEventState("赌博机活了，并向你冲了过来");
        PBEventState s9 = new PBEventState("你摧毁了赌博机，获得了里面所有奖品");
        s9.exitJobs = new PBJob[]{ BattleWin };
        PBEventState s10 = new PBEventState("你被赌博机吃了");
        s10.exitJobs = new PBJob[]{ BattleFail };
        PBBattleState s11 = new PBBattleState();

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
        PBEventAction a7 = new PBEventAction();
        PBEventAction a8 = new PBEventAction();
        a7.AddTransition(s9, 1);
        a8.AddTransition(s10, 1);
        PBEventAction a9 = new PBEventAction("战斗");
        a9.AddTransition(s11, 1);

        s1.AddAction(a1, a2);
        s2.AddAction(a3, a4, a6);
        s4.AddAction(a5);
        s5.AddAction(a5);
        s6.AddAction(a5);
        s7.AddAction(a5);
        s8.AddAction(a9);
        s11.AddAction(a7, a8);

        gmEvent = new PBEvent(s1, "赌博机事件", gm);
	}

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
            GameEventManager.instance.StartEvent(gm, gmEvent);
    }

    // ------ Public Functions ------

    // ------ Private Functions ------
    private void InitEvent(){

    }

    private void BattleWin(){
        UIManager.instance.AddInfoInBoard("你把" + gm.name + "砍死了");
        Destroy(gameObject, 1f);
    }

    private void BattleFail(){
        UIManager.instance.AddInfoInBoard("你被" + gm.name + "砍死了");
        UIManager.instance.AddInfoInBoard("你被阿畅复活了");
        
        // Reset event
        gmEvent.Reset();
        // Reset property       
        Data.player.Reset();
        gm.Reset();
    }
}
