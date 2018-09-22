/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ------ Event Functions ------
    void Awake(){
        if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject); 
    }

    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}

    void Update () {
		rb2d.position += speed * joystick.Direction * Time.deltaTime;
	}

    // ------ Public Functions ------

    // ------ Private Functions ------

}
