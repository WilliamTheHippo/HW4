using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpSpeed;

	Rigidbody2D rb;
	float horizontal;
	bool jumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontal = 0f;
        jumping = false;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
        	jumping = true;
        }
    }

    void FixedUpdate()
    {
    	rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    	if(jumping)
    	{
    		rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    		jumping = false;
    	}
    }
}
