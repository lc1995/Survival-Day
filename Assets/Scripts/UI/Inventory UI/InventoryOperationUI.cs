/*******************************************
* Description
* This class is repsonsible for ui of inventory operation board.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOperationUI : MonoBehaviour {

    // ------ Public Variables ------
    public InventoryUI inventoryUI;

    public RectTransform operationBoard;
    public Text description;

    [Header("Operation Buttons")]
    public Button eatBtn;
    public Button equipBtn;
    public Button decomposeBtn;
    public Button abandonBtn;



    [Header("Abandom UI")]
    public InventoryAbandomUI abandomUI;
    public RectTransform abandomBoard;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private InventoryButtonUI ibtn;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    /// <summary>
    /// Initialize the operation board for specific inventory
    /// </summary>
    /// <param name="ibtn">Referenced InventoryButtonUI</param>
    public void Initialize(InventoryButtonUI ibtn){
        this.ibtn = ibtn;

        // Set pivot and position of the board
        if(Input.mousePosition.x + operationBoard.rect.width > Display.main.systemWidth){
            if(Input.mousePosition.y - operationBoard.rect.height > 0)
                operationBoard.pivot = new Vector2(1, 1);
            else
                operationBoard.pivot = new Vector2(1, 0);
        }else{
            if(Input.mousePosition.y - operationBoard.rect.height > 0)
                operationBoard.pivot = new Vector2(0, 1);
            else
                operationBoard.pivot = new Vector2(0, 0);
        }
        operationBoard.position = Input.mousePosition;

        // Update buttons
        System.Type inventoryType = ibtn.inventory.GetType();
        if((inventoryType == typeof(Weapon) || inventoryType == typeof(Armor) || inventoryType == typeof(Accessory)) &&
        !Data.player.HasEquip(ibtn.inventory)){
            equipBtn.interactable = true;
        }else{
            equipBtn.interactable = false;
        }
        if(inventoryType == typeof(Food)){
            eatBtn.interactable = true;
        }else{
            eatBtn.interactable = false;
        }
        decomposeBtn.interactable = false;
        if(Data.player.HasEquip(ibtn.inventory)){
            abandonBtn.interactable = false;
        }else{
            abandonBtn.interactable = true;
        }

        // Update text
        description.text = ibtn.inventory.description;
    }

    public void Equip(){
        System.Type inventoryType = ibtn.inventory.GetType();
        if(inventoryType == typeof(Weapon)){
            Data.player.Equip(ibtn.inventory as Weapon);
        }else if(inventoryType == typeof(Armor)){
            Data.player.Equip(ibtn.inventory as Armor);
        }else{
            Data.player.Equip(ibtn.inventory as Accessory);
        }

        inventoryUI.listUI.UpdateUI();
        inventoryUI.currentUI.UpdateUI();
        gameObject.SetActive(false);
    }

    public void Eat(){
        Data.player.Eat(ibtn.inventory as Food);
        Data.player.ConsumeInventory(ibtn.inventory, 1);

        inventoryUI.listUI.UpdateUI();
        inventoryUI.currentUI.UpdateUI();
        gameObject.SetActive(false);
    }

    public void TryAbandom(){
        abandomBoard.gameObject.SetActive(true);
        abandomUI.Init(ibtn.inventory, Data.player.inventories[ibtn.inventory]);
    }

    // ------ Private Functions ------

}
