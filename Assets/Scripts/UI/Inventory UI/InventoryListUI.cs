/*******************************************
* Description
* This class is responsible for managing the list UI of all inventories of the player.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InventoryListUI : MonoBehaviour {

    // ------ Public Variables ------
    public InventoryUI inventoryUI;

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

    // ------ Public Functions ------
    public void ShowAllInventories(){
        HideAllInventories();
        foreach(Inventory inv in Data.player.inventories.Keys){
            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = inventoryUI;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }

    public void ShowEquipments(){
        HideAllInventories();
        foreach(Inventory inv in Data.player.inventories.Keys){
            if(inv.GetType() != typeof(Weapon) && inv.GetType() != typeof(Armor) &&
            inv.GetType() != typeof(Accessory))
                continue;

            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = inventoryUI;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }

    public void ShowFoods(){
        HideAllInventories();
        foreach(Inventory inv in Data.player.inventories.Keys){
            if(inv.GetType() != typeof(Food))
                continue;

            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = inventoryUI;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }


    public void ShowItems(){
        HideAllInventories();
        foreach(Inventory inv in Data.player.inventories.Keys){
            if(inv.GetType() != typeof(Item))
                continue;

            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = inventoryUI;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }

    public void ShowOthers(){
        HideAllInventories();
        foreach(Inventory inv in Data.player.inventories.Keys){
            if(inv.GetType() != typeof(Material))
                continue;

            GameObject go = objectPool.GetObject();
            go.transform.SetParent(inventoryPanel);
            go.GetComponent<InventoryButtonUI>().inventoryUI = inventoryUI;

            InventoryButtonUI buttonUI = go.GetComponent<InventoryButtonUI>();
            buttonUI.inventory = inv;
            buttonUI.UpdateUI();
        }
    }

    public void HideAllInventories(){
        foreach(Transform btn in inventoryPanel.GetComponentsInChildren<Transform>()){
            if(btn.GetComponent<InventoryButtonUI>())
                objectPool.ReturnObject(btn.gameObject);
        }
    }



    // ------ Private Functions ------

}
