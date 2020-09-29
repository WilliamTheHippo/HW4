using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
	public float time;
	float timer;

	void Start()
	{
		timer = time;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if(timer <= 0) Destroy(this.gameObject);
	}
}
