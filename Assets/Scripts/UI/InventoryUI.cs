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

    public Transform inventoryPanel;
    public SimpleObjectPool objectPool;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {

	}

    void Update () {
		
	}

    public override void Enter(){
        // Initialize all inventories
        foreach(Inventory inv in Data.player.inventories.Keys){
            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = this;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }

    public override void Exit(){
        // Release all buttons
        foreach(Transform btn in inventoryPanel.GetComponentsInChildren<Transform>()){
            if(btn.GetComponent<InventoryButtonUI>())
                objectPool.ReturnObject(btn.gameObject);
        }
    }

    // ------ Public Functions ------
    public void ActivateOperationPanel(Inventory inventory){
        operationUI.gameObject.SetActive(true);
        operationUI.Initialize(inventory);
    }

    public void DisativateOperationPanel(){
        operationUI.gameObject.SetActive(false);
    }

    // ------ Private Functions ------

}
