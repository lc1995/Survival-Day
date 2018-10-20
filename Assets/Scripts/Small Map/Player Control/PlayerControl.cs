/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour {

    public static PlayerControl instance;

    // ------ Public Variables ------
    [Range(0.1f, 20f)]
    public float speed = 2f;

    public Joystick joystick;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer sr;

    // ------ Event Functions ------
    void Awake(){
        if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject); 
    }

    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}

    void Update () {
        // Player Movement
        if(rb2d.bodyType == RigidbodyType2D.Dynamic)
		    rb2d.position += speed * joystick.Direction * Time.deltaTime;

        // Player Animation
        if(joystick.Direction == Vector2.zero){
            animator.SetBool("IsMovingH", false);
            animator.SetBool("IsMovingV", false);
        }else if(Mathf.Abs(joystick.Direction.x) >= Mathf.Abs(joystick.Direction.y)){
            animator.SetBool("IsMovingH", true);
            animator.SetBool("IsMovingV", false);
        }else{
            animator.SetBool("IsMovingV", true);
            animator.SetBool("IsMovingH", false);
        }

        // Player Sprite Flipping
        if(joystick.Direction.x >= 0)
            sr.flipX = true;
        else
            sr.flipX = false;
	}

    // ------ Public Functions ------
    /// <summary>
    /// Place object
    /// </summary>
    /// <param name="pObject">GameObject to place</param>
    public void PlaceObject(InteractionPlaceObject ipo){
        StartCoroutine(IEPlaceObject(ipo));
    }

    // ------ Private Functions ------
    private IEnumerator IEPlaceObject(InteractionPlaceObject ipo){
        animator.SetBool("IsInteraction", true);

        yield return new WaitForSeconds(ipo.duration);

        animator.SetBool("IsInteraction", false);
        Instantiate(ipo.objectToPlace, transform.position, Quaternion.identity);
    }
}
