using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
	public Player player;

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag == "Platform" || c.tag == "BouncyThing")
		{
			player.onGround = true;
			player.canMoveInAir = true;
		}
		if(c.tag == "Debuff") player.canMoveInAir = false;
		if(c.tag == "Bouncy") player.Bounce();
		if(c.tag == "DebuffAndBouncy")
		{
			player.Bounce();
			player.canMoveInAir = false;
		}
		if(c.tag == "Enemy")
		{
			//play sound?
			Debug.Log("not implemented yet");
		}
		if(c.tag == "Untagged") Debug.LogWarning("Untagged element!", c);
	}

	void OnTriggerExit2D(Collider2D c)
	{
		player.onGround = false;
	}
}
