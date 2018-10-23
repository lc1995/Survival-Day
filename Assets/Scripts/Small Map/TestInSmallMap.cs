/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInSmallMap : MonoBehaviour {

    // ------ Public Variables ------
    public List<SmallMapObject> zombies;
    public SmallMapObject gamblingMachine;
    public SmallMapObject bag;
    public GameObject trapObject;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
        Data.inBigMap = false;

        Data.player = new Character("李菊", true);

        foreach(Attack atk in Data.AllAttacks.Values){
            foreach(ActionType atkType in atk.types){
                if(atkType == ActionType.MeleeWeapon){
                    Data.player.attackPool.Add(atk);
                    break;
                }   
            }
        }

		foreach(SmallMapObject zombie in zombies){
            InitZombie(zombie);
        }
        InitGamblingMachine(gamblingMachine);

        InitTrapInteraction();

        InitTestInventory();

        InitTestBag(bag);
	}

    void Update () {
		
	}

    // ------ Public Functions ------

    // ------ Private Functions ------
    private void InitTrapInteraction(){
        InteractionPlaceObject trapInteraction = new InteractionPlaceObject("放置陷阱", trapObject);
        InteractionManager.instance.AddInteraction(trapInteraction);
    }

    private void InitZombie(SmallMapObject zombie){
        zombie.character = new Character("僵尸");

        Weapon sword = new Weapon(777, WeaponType.Melee);
        sword.name = "【美工刀】";
        zombie.character.weapon = sword;

        foreach(Attack atk in Data.AllAttacks.Values){
            foreach(ActionType atkType in atk.types){
                if(atkType == ActionType.MeleeWeapon || atkType == ActionType.Magic){
                    zombie.character.attackPool.Add(atk);
                    break;
                }   
            }
        }

        PBEventState s1 = new PBEventState("你遭遇了" + zombie.character.name);
        PBBattleState s2 = new PBBattleState();
        PBEventState s3 = new PBEventState("你把" + zombie.character.name + "砍死了");
        s3.exitJobs = new PBJob[]{ delegate{ BattleWin(zombie); } };
        PBEventState s4 = new PBEventState("你被" + zombie.character.name + "啃死了");
        s4.exitJobs = new PBJob[]{ delegate{ BattleFail(zombie);} };
        
        PBEventAction a1 = new PBEventAction("战斗");
        a1.AddTransition(s2, 1);
        PBEventAction a2 = new PBEventAction("");
        a2.AddTransition(s3, 1);
        PBEventAction a3 = new PBEventAction("");
        a3.AddTransition(s4, 1);

        s1.AddAction(a1);
        s2.AddAction(a2, a3);

        zombie.pBEvent = new PBEvent(s1, "僵尸事件", zombie.character);
        zombie.ai = new ZombieAI(4f, 1f, 2f, 1f);
    }

    private void InitGamblingMachine(SmallMapObject gm){
        gm.character = new Character("赌博机");

        Weapon wand = new Weapon(999, WeaponType.Magic);
        wand.name = "【无用大棒】";
        gm.character.weapon = wand;

        foreach(Attack atk in Data.AllAttacks.Values){
            foreach(ActionType atkType in atk.types){
                if(atkType == ActionType.Magic){
                    gm.character.attackPool.Add(atk);
                    break;
                }   
            }
        }

        PBEventState s1 = new PBEventState("遇到一个赌博机。");
        PBEventState s2 = new PBEventState("看起来这台老虎机还能用。");
        PBEventState s3 = new PBEventState("你离开了。");
        s3.exitJobs = new PBJob[]{ delegate{ Leave(gm); } };
        PBEventState s4 = new PBEventState("获得资源。");
        PBEventState s5 = new PBEventState("颗粒无收。");
        PBEventState s6 = new PBEventState("获得大笔金钱。");
        PBEventState s7 = new PBEventState("中了超级大乐透。");
        PBEventState s8 = new PBEventState("赌博机活了，并向你冲了过来");
        PBEventState s9 = new PBEventState("你摧毁了赌博机，获得了里面所有奖品");
        s9.exitJobs = new PBJob[]{ delegate{ BattleWin(gm); } };
        PBEventState s10 = new PBEventState("你被赌博机吃了");
        s10.exitJobs = new PBJob[]{ delegate{ BattleFail(gm); } };
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

        gm.interaction = new InteractionEvent("赌博机事件", new PBEvent(s1, "赌博机事件", gm.character), true);
        gm.ai = new StaticAI();
    }

    private void InitTestInventory(){
        // Test All Equipments
        foreach(Inventory inv in Data.AllInventories.Values){
            Data.player.ObtainInventory(inv, Random.Range(1, 4));
        }

        Data.player.Equip(Data.AllWeapons[2001]);
        Data.player.Equip(Data.AllArmors[1001]);
        Data.player.Equip(Data.AllAccessories[15001]);
    }

    private void InitTestBag(SmallMapObject bag){
        // 探险家之剑
        Weapon weapon = Data.AllWeapons[2001];
        InteractionObtainObject swordBag = new InteractionObtainObject("捡起" + weapon.name, weapon, 1, bag);

        bag.interaction = swordBag;
    }

    private void BattleWin(SmallMapObject smo){
        UIManager.instance.AddInfoInBoard("你把" + smo.character.name + "砍死了");
        smo.DestroyItself();
    }

    private void BattleFail(SmallMapObject smo){
        UIManager.instance.AddInfoInBoard("你被" + smo.character.name + "砍死了");
        UIManager.instance.AddInfoInBoard("你被阿畅复活了");
        
        // Reset event
        if(smo.pBEvent != null)
            smo.pBEvent.Reset();
        // Reset interaction
        if(smo.interaction != null && smo.interaction.GetType() == typeof(InteractionEvent))
            ((InteractionEvent) smo.interaction).Reset();
        // Reset property       
        Data.player.Reset();
        smo.character.Reset();
    }

    private void Leave(SmallMapObject smo){
        // Reset event
        if(smo.pBEvent != null)
            smo.pBEvent.Reset();
        // Reset interaction
        if(smo.interaction != null && smo.interaction.GetType() == typeof(InteractionEvent))
            ((InteractionEvent) smo.interaction).Reset();
    }

}
