/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    // ------ Public Variables ------
    public Character zombie;


    // ------ Shared Variables ------
    public PBEvent zombieEvent { get; private set; }

    // ------ Private Variables ------
    
    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		zombie = new Character("Zombie");

        InitEvent();
	}

    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
            GameEventManager.instance.StartEvent(zombie, zombieEvent);
    }

    // ------ Public Functions ------
    

    // ------ Private Functions ------
    private void InitEvent(){
        PBEventState s1 = new PBEventState("你遭遇了" + zombie.name);
        PBBattleState s2 = new PBBattleState("开始战斗");
        s2.enterJobs = new PBJob[]{ BattleEnter };
        PBEventState s3 = new PBEventState("你把" + zombie.name + "砍死了");
        PBEventState s4 = new PBEventState("你被" + zombie.name + "啃死了");
        
        PBEventAction a1 = new PBEventAction("战斗");
        a1.AddTransition(s2, 1);
        PBEventAction a2 = new PBEventAction("...");
        a2.AddTransition(s3, 1);
        PBEventAction a3 = new PBEventAction("...");
        a3.AddTransition(s4, 1);

        s1.AddAction(a1);
        s2.AddAction(a2, a3);

        zombieEvent = new PBEvent(s1, "僵尸事件");
    }

    private void BattleEnter(){
        BattleManager.instance.StartBattle(Data.player, zombie);
    }

    private void BattleWin(){
        Destroy(gameObject);
    }

    private void BattleFail(){
        Destroy(gameObject);
    }
}
