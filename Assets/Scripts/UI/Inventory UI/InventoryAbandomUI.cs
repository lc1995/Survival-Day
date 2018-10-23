/*******************************************
* Description
* This class is responsible for the UI element of confirm/cancel inventory abandom
* It also allows player to choose the abandom number
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAbandomUI : MonoBehaviour {

    // ------ Public Variables ------
    public InventoryOperationUI operationUI;
    public Scrollbar scrollbar;
    public Text currentValue;
    public Text minValue;
    public Text maxValue;

    [Header("Bag Prefab")]
    public GameObject bagPrefab;
    public Transform container;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private Inventory inventory;
    private int maxNumber;
    private int currentNumber;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void Init(Inventory inventory, int maxNumber){
        this.inventory = inventory;
        this.maxNumber = maxNumber;

        currentNumber = maxNumber;

        minValue.text = "0";
        maxValue.text = maxNumber.ToString();
        currentValue.text = maxNumber.ToString();

        scrollbar.value = 1;
        scrollbar.numberOfSteps = maxNumber + 1;
    }

    public void Confirm(){
        // Abandom 0 inventory means cancel
        if(currentNumber == 0)
            Cancel();

        // Consume
        Data.player.ConsumeInventory(inventory, currentNumber);

        // Create inventory bag in small map
        GameObject go = Instantiate(bagPrefab, PlayerControl.instance.transform.position, Quaternion.identity);
        go.transform.SetParent(container);
        SmallMapObject smo = go.GetComponent<SmallMapObject>();
        InteractionObtainObject interaction = new InteractionObtainObject("捡起" + inventory.name,
            inventory, currentNumber, smo);
        smo.interaction = interaction;

        // Show info on top info board
        UIManager.instance.AddInfoInBoard("你丢弃了" + inventory.name + " x " + currentNumber);

        // Disactive boards
        gameObject.SetActive(false);

        operationUI.inventoryUI.listUI.UpdateUI();
        operationUI.inventoryUI.currentUI.UpdateUI();
        operationUI.gameObject.SetActive(false);
    }

    public void Cancel(){
        gameObject.SetActive(false);
        operationUI.gameObject.SetActive(false);
    }

    public void OnValueChanged(){
        currentNumber = (int)Mathf.Round(scrollbar.value * maxNumber);
        currentValue.text = currentNumber.ToString();
    }

    // ------ Private Functions ------

}
