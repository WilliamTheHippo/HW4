using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpSpeed;
	public bool onGround;
	public bool canMoveInAir;
	[Tooltip("Physics engine hackiness; higher value stops bouncing sooner to reduce jitter.")]
	public float bounceThreshold;

	Rigidbody2D rb;
	float horizontal;
	bool jumping;
	bool bouncing;
	float lastYVelocity;
	float bounceVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontal = 0f;
        jumping = false;
        canMoveInAir = true;
        bouncing = false;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && onGround) jumping = true;
    }

    void FixedUpdate()
    {
    	lastYVelocity = rb.velocity.y;
    	if(canMoveInAir || onGround)
    	{
    		rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    	}
    	if(jumping)
    	{
    		rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    		jumping = false;
    	}
    	if(bouncing)
    	{
    		rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);
    		bouncing = false;
    	}
    }

    public void Bounce()
    {
    	//play sound?
    	//Debug.Log(lastYVelocity);
    	if(Mathf.Abs(lastYVelocity) > bounceThreshold)
    	{
    		bounceVelocity = -lastYVelocity;
    		bouncing = true;
    	}
    	else
    	{
    		jumping = true;
    	}
    }
}
