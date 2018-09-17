/*******************************************
* Description
* UI Manager is responsible for managing all UI elements in the scene
* UI Manager is a singleton which provides a global accessor
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private ScrollRect infoSR;
    private Text infoText;

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

		InitUIElement();
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void AddButtonsListeners(){

    }

    public void AddInfoInBoard(string str){
        infoText.text += str;

		Canvas.ForceUpdateCanvases();
		infoSR.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
    }
    
    public void OnBoardBtnClick(GameObject go){
        go.SetActive(true);
    }

    public void OnBoardExitClick(GameObject go){
        go.SetActive(false);
    }

    // ------ Private Functions ------
    private void OnInfoBoardExtend(){
        StartCoroutine(IEInfoBoardExtend());
    }

    private void OnInfoBoardCurtail(){
        StartCoroutine(IEInfoBoardCurtail());
    }

    private void InitUIElement(){
        infoBoard.onClick.AddListener(OnInfoBoardExtend);
    }

    private IEnumerator IEInfoBoardExtend(){
        infoBoard.onClick.RemoveAllListeners();

        RectTransform rt = infoBoard.GetComponent<RectTransform>();
        while(rt.sizeDelta.y < infoBoardMaxHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + infoBoardSpeed);
            yield return null;
        }

        infoBoard.onClick.AddListener(OnInfoBoardCurtail);
    }

    private IEnumerator IEInfoBoardCurtail(){
        infoBoard.onClick.RemoveAllListeners();

        RectTransform rt = infoBoard.GetComponent<RectTransform>();
        while(rt.sizeDelta.y > infoBoardMinHeight){
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - infoBoardSpeed);
            yield return null;
        }

        infoBoard.onClick.AddListener(OnInfoBoardExtend);
    }    
}
