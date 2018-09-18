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
        PBBattleState s2 = new PBBattleState();
        PBEventState s3 = new PBEventState("你把" + zombie.name + "砍死了");
        s3.exitJobs = new PBJob[]{ BattleWin };
        PBEventState s4 = new PBEventState("你被" + zombie.name + "啃死了");
        s4.exitJobs = new PBJob[]{ BattleFail };
        
        PBEventAction a1 = new PBEventAction("战斗");
        a1.AddTransition(s2, 1);
        PBEventAction a2 = new PBEventAction("");
        a2.AddTransition(s3, 1);
        PBEventAction a3 = new PBEventAction("");
        a3.AddTransition(s4, 1);

        s1.AddAction(a1);
        s2.AddAction(a2, a3);

        zombieEvent = new PBEvent(s1, "僵尸事件", zombie);
    }

    private void BattleWin(){
        UIManager.instance.AddInfoInBoard("你把" + zombie.name + "砍死了");
        Destroy(gameObject, 1f);
    }

    private void BattleFail(){
        UIManager.instance.AddInfoInBoard("你被" + zombie.name + "砍死了");
        UIManager.instance.AddInfoInBoard("你被阿畅复活了");
        
        // Reset event
        zombieEvent.Reset();
        // Reset property       
        Data.player.Reset();
        zombie.Reset();
    }

}
