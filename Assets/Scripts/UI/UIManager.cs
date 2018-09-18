﻿/*******************************************
* Description
* UI Manager is responsible for managing all UI elements in the scene
* UI Manager is a singleton which provides a global accessor
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    // ------ Public Variables ------
    public Button stateBtn;
    public Button inventoryBtn;
    public Button buildBtn;
    public Button missionBtn;
    public Button infoBoard;
    public float infoBoardMinHeight = 200f;
    public float infoBoardMaxHeight = 1500f;
    public float infoBoardSpeed = 50f;
    public GameObject stateBoard;
    public GameObject inventoryBoard;
    public GameObject buildBoard;
    public GameObject missionBoard;
    public RectTransform joystick;
    [Header("Event UI")]
    public GameObject eventBoard;
    public Text eventInfoText;
    public List<Button> eventBtns;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private ScrollRect infoSR;
    private Text infoText;
    private ScrollRect eventSR;

    // ------ Required Components ------

    // ------ Event Functions ------
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // DontDestroyOnLoad(this);
    }

    void Start () {
        infoSR = infoBoard.GetComponent<ScrollRect>();
        infoText = infoBoard.GetComponentInChildren<Text>();
        eventSR = eventBoard.GetComponentInChildren<ScrollRect>();

		InitUIElement();
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    #region Event Part
    /// <summary>
    /// Add information on top InfoBoard
    /// </summary>
    /// <param name="str">Information to show</param>
    public void AddInfoInBoard(string str){
        if(infoText.text.Length == 0)
            infoText.text = str;
        else
            infoText.text += "\n" + str;

		Canvas.ForceUpdateCanvases();
		infoSR.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
    }
    
    /// <summary>
    /// Show/Hide event board
    /// </summary>
    /// <param name="show">Whether to show or hide</param>
    public void ShowEventBoard(bool show=true){
        eventBoard.SetActive(show);
    }

    /// <summary>
    /// Add listener on specific event button
    /// </summary>
    /// <param name="index">Index of button</param>
    /// <param name="callback">Callback of the event</param>
    public void AddEventBtnListener(int index, UnityAction callback){
        eventBtns[index].onClick.AddListener(callback);
    }

    /// <summary>
    /// Set the text showed in specific event button
    /// </summary>
    /// <param name="index">Index of button</param>
    /// <param name="str">String to show</param>
    public void SetEventBtnText(int index, string str){
        eventBtns[index].GetComponentInChildren<Text>().text = str;
    }

    /// <summary>
    /// Remove all listeners on specific event button
    /// </summary>
    /// <param name="index">Index of button</param>
    public void RemoveEventBtnListeners(int index){
        eventBtns[index].onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Add event info
    /// </summary>
    /// <param name="str">String to show</param>
    /// <param name="isAppend">Whether to set or append</param>
    public void SetEventInfo(string str, bool isAppend=false){
        if(!isAppend){
            eventInfoText.text = str;
        }else{
            eventInfoText.text += str;
        }

        Canvas.ForceUpdateCanvases();
		eventSR.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
    }

    /// <summary>
    /// Add dividing line in event info
    /// </summary>
    public void AddEventDividingLine(){
        eventInfoText.text += "---------------\n";
    }

    /// <summary>
    /// Get the number of event buttons
    /// </summary>
    public int GetEventBtnsNumber(){
        return eventBtns.Count;
    }
    #endregion

    /// <summary>
    /// Onclick callback of board button
    /// This will active the relative gameObject
    /// </summary>
    /// <param name="go">GameObject to active</param>
    public void OnBoardBtnClick(GameObject go){
        go.SetActive(true);
    }

    /// <summary>
    /// Onclick callback of baord button
    /// This will disative the ralative gameObject
    /// </summary>
    /// <param name="go">GameObject to disative</param>
    public void OnBoardExitClick(GameObject go){
        go.SetActive(false);
    }

    // ------ Private Functions ------
    /// <summary>
    /// Extend top InfoBoard
    /// </summary>
    private void OnInfoBoardExtend(){
        StartCoroutine(IEInfoBoardExtend());
    }

    /// <summary>
    /// Curtial top InfoBoard
    /// </summary>
    private void OnInfoBoardCurtail(){
        StartCoroutine(IEInfoBoardCurtail());
    }

    /// <summary>
    /// Initialize all UI elements in the scene
    /// </summary>
    private void InitUIElement(){
        // Add a dynamic listener on InfoBoard
        // It can't be permanent(created in editor) since it changes in gameplay
        infoBoard.onClick.AddListener(OnInfoBoardExtend);
    }

    /// <summary>
    /// IEnumerator for extention of top InfoBoard
    /// </summary>
    private IEnumerator IEInfoBoardExtend(){
        infoBoard.onClick.RemoveAllListeners();

        RectTransform rt = infoBoard.GetComponent<RectTransform>();
        while(rt.sizeDelta.y < infoBoardMaxHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + infoBoardSpeed);
            yield return null;
        }

        infoBoard.onClick.AddListener(OnInfoBoardCurtail);
    }

    /// <summary>
    /// Inumerator for curtial of top InfoBoard
    /// </summary>
    private IEnumerator IEInfoBoardCurtail(){
        infoBoard.onClick.RemoveAllListeners();

        RectTransform rt = infoBoard.GetComponent<RectTransform>();
        while(rt.sizeDelta.y > infoBoardMinHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - infoBoardSpeed);
            yield return null;
        }

        infoBoard.onClick.AddListener(OnInfoBoardExtend);

        // Reset vertical position of ScrollRoll to 0
        Canvas.ForceUpdateCanvases();
		infoSR.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
    }    
}