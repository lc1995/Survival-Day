/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
    private SpriteRenderer sr;
    private Animator animator;

    // ------ Event Functions ------
    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Set default ai control
        if(ai == null)
            ai = new StaticAI();
	}

    void Update () {
        Vector2 direction = ai.GetDirection(transform, PlayerControl.instance.transform);
        if(rb2d.bodyType == RigidbodyType2D.Dynamic)
		    GetComponent<Rigidbody2D>().position += direction * speed * Time.deltaTime;

        // Update animation
        if(animator != null){
            if(direction != Vector2.zero){
                animator.SetBool("IsMoving", true);
            }else{
                animator.SetBool("IsMoving", false);
            }
        }
        
        // Flipping
        if(direction.x <= 0)
            sr.flipX = false;
        else
            sr.flipX = true;
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
    public void DestroyItself(){
        Destroy(gameObject);
    }

    // ------ Private Functions ------

}
