/*******************************************
* Description
* Interaction Manager managers all interactions in the scene.
* Interaction may from event, label, race or inventory
* Interaction Manager is a singleton
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public static InteractionManager instance;

    // ------ Public Variables ------

    // ------ Shared Variables ------
    public List<Interaction> interactions { get; private set; }

    // ------ Private Variables ------

    // ------ Required Components ------

    // ------ Event Functions ------
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
		interactions = new List<Interaction>();
	}

    void Update () {
		
	}

    // ------ Public Functions ------
    public void AddInteraction(Interaction interaction){
        interactions.Add(interaction);
    }

    public void RemoveInteraction(Interaction interaction){
        interactions.Remove(interaction);
    }

    // ------ Private Functions ------

}
