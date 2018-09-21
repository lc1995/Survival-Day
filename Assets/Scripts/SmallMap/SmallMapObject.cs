/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapObject : MonoBehaviour {

    // ------ Public Variables ------
    public Character character;
    public PBEvent pBEvent;
    public Interaction interaction;
    public SmallMapObjectAI ai;
    public float speed;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------
    private Rigidbody2D rb2d;

    // ------ Event Functions ------
    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}

    void Update () {
        if(rb2d.bodyType == RigidbodyType2D.Dynamic)
		    GetComponent<Rigidbody2D>().position += ai.GetDirection(transform, PlayerControl.instance.transform) * speed * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D other){    
        if(other.tag == "Player"){
            // Trigger event
            if(pBEvent != null)
                GameEventManager.instance.StartEvent(pBEvent);

            // Trigger interaction
            if(interaction != null)
                InteractionManager.instance.AddInteraction(interaction); 
        }
          
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player")
            InteractionManager.instance.RemoveInteraction(interaction);
    }

    // ------ Public Functions ------

    // ------ Private Functions ------

}
