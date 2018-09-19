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
    [SerializeField]
    public SmallMapObjectAI ai;
    public float speed;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Start () {
		
	}

    void Update () {
		GetComponent<Rigidbody2D>().velocity = ai.GetDirection(transform, PlayerControl.instance.transform) * speed;
	}

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(interaction.isActive)
                InteractionManager.instance.AddInteraction(interaction);
            else
                interaction.Interact();
        }
            
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player" && interaction.isActive)
            InteractionManager.instance.RemoveInteraction(interaction);
    }

    // ------ Public Functions ------

    // ------ Private Functions ------

}
