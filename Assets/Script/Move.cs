using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Globalization;

public class Move1 : MonoBehaviour
{
    private float moveSpeed = 5;

    public float jumpForce = 10;
    public float pos_y;
    public float rotation;
    public double sin;
    public double cos;
    [SerializeField] bool isGround;
    Vector2 moveInput;
    
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        pos_y = rb.position.y;
    }


// Update is called once per frame
    void Update()
    {
        rotation = rb.rotation;
        float keyboardX = Input.GetAxis("Horizontal");
        float keyboardY = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(keyboardX, keyboardY).normalized;
        double angle = rb.rotation;
        double radyan = angle * (Math.PI / 180);
        sin = Math.Sin(angle);
        cos = Math.Cos(angle);
        Vector2 angl = new Vector2((float)cos, (float)sin);
        transform.Translate(moveDirection  * moveSpeed * Time.deltaTime);

        // if (isGround)
        // {
        //     Vector2 moveUp = new Vector2(0, keyboardY).normalized;
        //     rb.AddForce(moveUp);
        // }
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }

        if (rb.position.y < -10)
        {
            Vector2 pose = new Vector2(0,0);
            rb.MovePosition(pose);
        }
    }


    private void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = false;
        }
    }
}



