using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Physics")]
	public float moveSpeed;
	public float jumpSpeed;
	public bool onGround;
	public bool canMoveInAir;
	[Tooltip("Physics engine hackiness; higher value stops bouncing sooner to reduce jitter.")]
	public float bounceThreshold;

	[Header("Sounds")]
	[Tooltip("Plays on death")]
	public AudioClip beep;
	[Tooltip("Plays on bounce")]
	public AudioClip boing;
	[Tooltip("Plays on powerup")]
	public AudioClip ping;

	[Header("Sprites")]
	public Sprite poweredUpSprite;

	SpriteRenderer sr;
	AudioSource sound;
	Rigidbody2D rb;
	
	float horizontal;
	bool jumping;
	bool bouncing;
	bool poweredUp;
	float lastYVelocity;
	float bounceVelocity;

    void Start()
    {
    	sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        horizontal = 0f;
        jumping = false;
        canMoveInAir = true;
        bouncing = false;
        poweredUp = false;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetButton("Jump") && onGround)
        {
        	if(!poweredUp) jumping = true;
        	else
        	{
        		//
        	}
        }
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
    	sound.clip = boing;
    	sound.Play();
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

    void OnTriggerEnter2D(Collider2D c)
    {
    	if(c.tag == "Enemy")
    	{
    		Destroy(c.gameObject);
	    	sound.clip = beep;
	    	sound.Play();
	    	transform.position = new Vector3(0f,0f,0f);
	    }
	    if(c.tag == "PowerUp")
	    {
	    	Destroy(c.gameObject);
	    	sound.clip = ping;
	    	sr.sprite = poweredUpSprite;
	    	poweredUp = true;
	    }
    }
}
