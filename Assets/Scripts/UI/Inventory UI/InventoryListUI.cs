/*******************************************
* Description
* This class is responsible for managing the list UI of all inventories of the player.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum FilterMode{
    All,
    Items,
    Equipments,
    Foods,
    Others
}

public class InventoryListUI : MonoBehaviour {

    // ------ Public Variables ------
    public InventoryUI inventoryUI;

    public Transform inventoryPanel;
    public SimpleObjectPool objectPool;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private FilterMode currentFilter;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void UpdateUI(){
        switch(currentFilter){
            case FilterMode.All:
                ShowAllInventories();
                break;
            case FilterMode.Equipments:
                ShowEquipments();
                break;
            case FilterMode.Foods:
                ShowFoods();
                break;
            case FilterMode.Items:
                ShowItems();
                break;
            case FilterMode.Others:
                ShowOthers();
                break;
        }
    }

    public void ShowAllInventories(){
        currentFilter = FilterMode.All;
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
        currentFilter = FilterMode.Equipments;
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
        currentFilter = FilterMode.Foods;
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
        currentFilter = FilterMode.Items;
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
        currentFilter = FilterMode.Others;
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
