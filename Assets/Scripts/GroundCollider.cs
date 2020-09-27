using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
	public Player player;

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag == "Platform" || c.tag == "JumpyThing")
		{
			player.onGround = true;
		}
		if(c.tag == "Platform") player.canMoveInAir = true;
		if(c.tag == "Debuff") player.canMoveInAir = false;
		if(c.tag == "Bouncy") player.Bounce();
		if(c.tag == "DebuffAndBouncy")
		{
			player.Bounce();
			player.canMoveInAir = false;
		}
		//if(c.tag == "Enemy") player.Reset();
		if(c.tag == "Untagged") Debug.LogWarning("Untagged element!", c);
	}

	void OnTriggerExit2D(Collider2D c)
	{
		player.onGround = false;
	}
}
