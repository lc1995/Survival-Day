/*******************************************
* Description
* This class is repsonsible for UI of current inventory.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCurrentUI : MonoBehaviour {

    // ------ Public Variables ------
    public InventoryUI inventoryUI;

    public Text weightText;
    public Text moneyText;
    public Text weaponText;
    public Text armorText;
    public Text accessoryText;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		UpdateUI();
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void UpdateUI(){
        weightText.text = Data.player.currentWeight + "/" + Data.player.maxWeight;

        moneyText.text = Data.player.money.ToString();

        if(Data.player.weapon != null)
            weaponText.text = Data.player.weapon.name;
        else
            weaponText.text = "未装备";

        if(Data.player.armor != null)
            armorText.text = Data.player.armor.name;
        else
            armorText.text = "未装备";

        if(Data.player.accessory != null)
            accessoryText.text = Data.player.accessory.name;
        else
            accessoryText.text = "未装备";
    }

    // ------ Private Functions ------

}
