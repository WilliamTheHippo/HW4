using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody2D spawn;
    public enum Direction {Left, Right, Up, Down};
    public Direction direction;
    public int speed;
    [Tooltip("Time between spawns in seconds.")]
    public int interval;

    void Update()
    {
    	if(Input.GetButtonDown("Fire1")) //left-ctrl
    	{
    		Rigidbody2D spawned =
    			Instantiate(spawn, transform.position, Quaternion.identity);
			if(direction == Direction.Up) spawned.velocity = new Vector2(0, speed);
			if(direction == Direction.Down) spawned.velocity = new Vector2(0, -speed);
			if(direction == Direction.Left) spawned.velocity = new Vector2 (-speed, 0);
			if(direction == Direction.Right) spawned.velocity = new Vector2(speed, 0);    	}
    }
}
