using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    bool crouch = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))       // this if and else if cases give the crouch functionality when the button is pressed only
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        
    }


    void FixedUpdate()
    {
        // Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        // Time.fixedDeltaTime is the amount of time last from the previous execution of this variable

    }
}
