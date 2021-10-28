using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem
{
	public int npcCount = 0;
	public int enemyCount = 0;
	public float timer = 0;
	public int score = 0;
	public int candyCount = 5;

	public void Update()
	{
		timer += Time.deltaTime;
	}

	public void Reset()
	{
		npcCount = 0;
		enemyCount = 0;
		timer = 0;
		score = 0;
		candyCount = 5;
	}
}
