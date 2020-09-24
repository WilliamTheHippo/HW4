using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
	public Player player;

	void OnTriggerStay2D(Collider2D c)
	{
		player.onGround = true;
	}

	void OnTriggerExit2D(Collider2D c)
	{
		player.onGround = false;
	}
}
