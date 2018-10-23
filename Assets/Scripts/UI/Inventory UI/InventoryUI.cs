/*******************************************
* Description
* UI manager of inventory.
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BoardBase {

    // ------ Public Variables ------
    public InventoryOperationUI operationUI;
    public InventoryListUI listUI;
    public InventoryCurrentUI currentUI;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {

	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public override void Enter(){
        currentUI.UpdateUI();
        listUI.ShowAllInventories();
    }

    public override void Exit(){
        listUI.HideAllInventories();
    }

    public void ActivateOperationPanel(InventoryButtonUI ibtn){
        operationUI.gameObject.SetActive(true);
        operationUI.Initialize(ibtn);
    }

    public void DisativateOperationPanel(){
        operationUI.gameObject.SetActive(false);
        operationUI.abandomUI.gameObject.SetActive(false);
    }

    // ------ Private Functions ------

}
