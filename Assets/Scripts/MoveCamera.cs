using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	public float moveSpeed;
	public enum Direction {Lateral,Vertical};
	public Direction direction;

    void OnTriggerStay2D(Collider2D c)
    {
    	if(c.tag == "Player")
    	{
    		Vector3 move = c.transform.position - Camera.main.transform.position;
    		if(direction == Direction.Lateral) move = new Vector3(move.x, 0f, 0f);
    		if(direction == Direction.Vertical) move = new Vector3(0f, move.y, 0f);
    		if(move.magnitude > 1) move = move.normalized;
    		Camera.main.transform.position += move * Time.deltaTime * moveSpeed;
    	}
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if(c.tag == "Player" &&
            (Camera.main.transform.position.y - c.transform.position.y > 10 || 
            Camera.main.transform.position.x - c.transform.position.x > 5))
        {
            Camera.main.transform.position = c.transform.position + new Vector3(0f,0f,-10f);
        }
    }
}
