/*******************************************
* Description
* This class is responsible for managing all UI elements in main menu
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    /// <summary>
    /// Callback when start button is clicked
    /// </summary>
    public void OnStartGame(){
        // For now(testing), it simply loads BigMap scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Callback when continue button is clicked
    /// </summary>
    public void OnContinueGame(){
        // For now(testing), it simply loads BigMap scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Callback when setting button is clicked
    /// </summary>
    public void OnSetting(){
        // For now(testing), it simply logs a simple message
        Debug.Log("Setting Button clicks.");
    }

    /// <summary>
    /// Callback when exit button is clicked
    /// </summary>
    public void OnExit(){
        // For now(testing), it simply logs a simple message
        Debug.Log("Exit button clicks");
    }

    // ------ Private Functions ------

}
