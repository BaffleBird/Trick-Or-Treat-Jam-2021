using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem
{
	public int npcCount = 0;
	public int enemyCount = 0;
	public float timer = 360;
	public int score = 0;
	public int candyCount = 5;
	bool endTheGame = false;


	public void Update()
	{
		if (npcCount < 10 && !endTheGame)
		{
			GameManager.instance.EndGame();
			endTheGame = true;
		}
	}

	public void Reset()
	{
		npcCount = 0;
		enemyCount = 0;
		timer = 360;
		score = 0;
		candyCount = 5;
	}
}
