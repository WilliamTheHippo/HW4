using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Physics")]
	public float moveSpeed;
	public float jumpSpeed;
	public float superJumpSpeed;
	public bool onGround;
	public bool canMoveInAir;
	[Tooltip("Physics engine hackiness; higher value stops bouncing sooner to reduce jitter.")]
	public float bounceThreshold;
	[Tooltip("Maximum charge level for powerup jump.")]
	public float maxCharge;
	public float chargeSpeed;

	[Header("Sounds")]
	[Tooltip("Plays on death")]
	public AudioClip beep;
	[Tooltip("Plays on bounce")]
	public AudioClip boing;
	[Tooltip("Plays on powerup")]
	public AudioClip ping;
	[Tooltip("Plays on jumpy thing")]
	public AudioClip blip;

	[Header("Sprites")]
	public Sprite defaultSprite;
	public Sprite poweredUpSprite;

	SpriteRenderer sr;
	AudioSource sound;
	Rigidbody2D rb;
	CircleCollider2D cc;
	
	float horizontal;
	bool jumping;
	bool superJumping;
	bool bouncing;
	bool poweredUp;
	float lastYVelocity;
	float bounceVelocity;

	bool chargingJump;
	float chargeLevel;

	float radius;

    void Start()
    {
    	sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        cc = GetComponent<CircleCollider2D>();
        horizontal = 0f;
        jumping = false;
        canMoveInAir = true;
        bouncing = false;
        poweredUp = false;
        chargingJump = false;
        chargeLevel = 0;
        radius = cc.radius;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetButton("Jump") && onGround)
        {
        	if(!poweredUp) jumping = true;
        	else
        	{
        		chargingJump = true;
        		if(chargeLevel < maxCharge) chargeLevel += chargeSpeed * Time.deltaTime;
        		float shrink = chargeLevel * 0.03f; //yeah, I know, magic number...
        		transform.localScale = new Vector3(1, 1-shrink, 1);
        		cc.radius = radius * (1-shrink);
        	}
        }
        if(Input.GetButtonUp("Jump") && chargingJump)
        {
        	sound.clip = boing;
        	sound.Play();
        	sr.sprite = defaultSprite;
        	chargingJump = false;
        	poweredUp = false;
        	superJumping = true;
        	transform.localScale = new Vector3(1,1,1);
        	cc.radius = radius;
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
    	if(superJumping)
    	{
    		rb.velocity = new Vector2(rb.velocity.x, superJumpSpeed * chargeLevel);
    		superJumping = false;
    		chargeLevel = 0;
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
    		//Destroy(c.gameObject);
	    	sound.clip = beep;
	    	sound.Play();
	    	transform.position = new Vector3(0f,0f,0f);
	    	Camera.main.transform.position = new Vector3(0f,0f,-10f);
	    }
	    if(c.tag == "JumpyThing")
	    {
	    	Destroy(c.gameObject, .05f);
	    	sound.clip = blip;
	    	if(!sound.isPlaying) sound.Play();
	    }
	    if(c.tag == "PowerUp")
	    {
	    	Destroy(c.gameObject);
	    	sound.clip = ping;
	    	sound.Play();
	    	sr.sprite = poweredUpSprite;
	    	poweredUp = true;
	    }
    }
}
