/*******************************************
* Description
* UI Manager is responsible for managing all UI elements in the scene
* UI Manager is a singleton which provides a global accessor
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour{

    public static UIManager instance;

    // ------ Public Variables ------
    [Header("Map Hub")]
    public GameObject mapHub;
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
    [Header("Interaction UI")]
    public GameObject interactionBoard;
    public GameObject interactionPanel;
    public SimpleObjectPool buttonsPool;
    public float interactionBoardMaxHeight = 800f;
    [Header("Big Map")]
    public float dragSpeed = 0.1f;
    public float leftBorder = -7.8f;
    public float rightBorder = 7.8f;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private ScrollRect infoSR;
    private Text infoText;
    private ScrollRect eventSR;
    private ScrollRect interactionSR;

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
        interactionSR = interactionBoard.GetComponentInChildren<ScrollRect>();

		InitUIElement();
	}

    void Update () {
		if(Data.inBigMap)
            CheckMapDrag();
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
        mapHub.SetActive(!show);
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

    /// <summary>
    /// Onclick callback of joystick
    /// </summary>
    /// <param name="isExtend">Whether to extend or curtial</param>
    public void OnJoystickClick(bool isExtend){
        if(isExtend)
            StartCoroutine(IEInteractionBoardExtend());
        else
            StartCoroutine(IEInteractionBoardCurtail());
    }
    
    // ------ Private Functions ------
    private void CheckMapDrag(){
        #if UNITY_IOS
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Camera.main.transform.position += new Vector3(-Input.GetTouch(0).deltaPosition.x, 0);
        }

        #elif UNITY_STANDALONE
        float horizontal = Input.GetAxis("Horizontal");
        if(Input.GetMouseButton(0)){
            
        }

        #elif UNITY_WEBGL
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Camera.main.transform.position += new Vector3(-Input.GetTouch(0).deltaPosition.x, 0);
        }else if(Input.GetMouseButton(0)){
            float horizontal = Input.GetAxis("Horizontal");
            Camera.main.transform.position += new Vector3(-horizontal * dragSpeed, 0);
            if(Camera.main.transform.position.x < leftBorder)
                Camera.main.transform.position = new Vector3(leftBorder, 0, -10f);
            if(Camera.main.transform.position.x > rightBorder)
                Camera.main.transform.position = new Vector3(rightBorder, 0, -10f);
        }
        #endif
    }
    
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
    /// IEnumerator for curtial of top InfoBoard
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

    /// <summary>
    /// IEnumerator for extension of interaction board
    /// </summary>
    private IEnumerator IEInteractionBoardExtend(){
        // Create buttons
        foreach(Interaction inte in InteractionManager.instance.interactions){
            GameObject go = buttonsPool.GetObject();
            go.GetComponentInChildren<Text>().text = inte.description;
            go.GetComponent<Button>().onClick.AddListener(delegate{ inte.Interact(); });
            go.transform.SetParent(interactionPanel.transform, false);
        }

        // Extension
        interactionBoard.SetActive(true);
        RectTransform rt = interactionSR.GetComponent<RectTransform>();
        while(rt.sizeDelta.y < interactionBoardMaxHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + infoBoardSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// IEnumerator for curtial of interaction board
    /// </summary>
    private IEnumerator IEInteractionBoardCurtail(){
        // Curtail
        RectTransform rt = interactionSR.GetComponent<RectTransform>();
        while(rt.sizeDelta.y > infoBoardMinHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - infoBoardSpeed);
            yield return null;
        }
        interactionBoard.SetActive(false);

        // Remove buttons
        foreach(Button btn in interactionPanel.GetComponentsInChildren<Button>()){
            btn.onClick.RemoveAllListeners();
            buttonsPool.ReturnObject(btn.gameObject);
        }
    }
}
