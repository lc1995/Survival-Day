﻿/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    // ------ Public Variables ------
    [Range(0.1f, 5f)]
    public float speed = 2f;

    public Joystick joystick;

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------
    private Rigidbody2D rb2d;

    // ------ Event Functions ------
    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}

    void Update () {
		rb2d.position += speed * joystick.Direction;
	}

    // ------ Public Functions ------

    // ------ Private Functions ------

}