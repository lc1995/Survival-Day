/*******************************************
* Description
* This class is responsible for the trap
* The trap can trap the character for a specific time when the character is closed
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    // ------ Public Variables ------
    public Sprite closeSprite;
    public float trapTime;

    // ------ Shared Variables ------

    // ------ Private Variables ------
    private bool isClosed;
    private bool playerHasEnter;

    // ------ Required Components ------
    private CircleCollider2D cc2d;
    private SpriteRenderer sr;

    // ------ Event Functions ------
    void Start () {
		cc2d = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        isClosed = false;
        playerHasEnter = false;
	}

    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        // Check if the trap is closed
        if(isClosed)
            return;

        // Check if it's first time that the player enters the range of trap
        // If it is, the trap would not be triggered
        if(!playerHasEnter && other.tag == "Player"){
            return;
        }

        Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
        if(rb2d != null && rb2d.bodyType == RigidbodyType2D.Dynamic){
            StartCoroutine(TrapForSeconds(rb2d, trapTime));

            sr.sprite = closeSprite;
            isClosed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(!playerHasEnter && other.tag == "Player"){
            playerHasEnter = true;
        }
    }

    // ------ Public Functions ------

    // ------ Private Functions ------
    /// <summary>
    /// IEnumerator to trap the character for a specific time in second
    /// </summary>
    /// <param name="chRb2d">Rigidbody2D of the character</param>
    /// <param name="time">Trap time in second</param>
    private IEnumerator TrapForSeconds(Rigidbody2D chRb2d, float time){
        chRb2d.transform.position = cc2d.bounds.center;
        chRb2d.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(time);

        chRb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}
