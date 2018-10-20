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

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void Initialize(Inventory inventory){
        // Set pivot and position of the board
        if(Input.mousePosition.x + operationBoard.rect.width > Display.main.systemWidth){
            operationBoard.pivot = new Vector2(1, 1);
        }else{
            operationBoard.pivot = new Vector2(0, 1);
        }
        operationBoard.position = Input.mousePosition;

        // Update text
        description.text = inventory.description;
    }


    // ------ Private Functions ------

}
