/*******************************************
* Description
* This script manages the ui behavior of intentory button.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonUI : MonoBehaviour {

    // ------ Public Variables ------
    public Inventory inventory;

    public InventoryUI inventoryUI;
    public Text nameWithNumberText;
    public Text weightText;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------
    

    // ------ Event Functions ------
    void Start () {
		
	}

    // ------ Public Functions ------
    public void UpdateUI(){
        if(Data.player.HasEquip(inventory)){
            nameWithNumberText.text = inventory.name + "（已装备）" + " x " + Data.player.inventories[inventory].ToString();
        }else{
            nameWithNumberText.text = inventory.name + " x " + Data.player.inventories[inventory].ToString();
        }
    
        weightText.text = inventory.weight.ToString() + "kg";     
    }

    public void OnButtonClick(){
        inventoryUI.ActivateOperationPanel(this);
    }

    // ------ Private Functions ------

}
