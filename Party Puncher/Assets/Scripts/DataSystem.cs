using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem
{
	public bool gameStart = false;
	public int npcCount = 0;
	public int enemyCount = 0;
	public float startTime = 180;
	public float timer = 0;
	public int score = 0;
	public int finalScore = 0;
	public int candyCount = 5;
	bool endTheGame = false;
	public int enemiesDefeated = 0;


	public void Update()
	{
		if (gameStart)
		{
			timer -= Time.deltaTime;
			if ((npcCount < 5 || timer <= 0) && !endTheGame)
			{
				if (timer < 0) timer = 0;
				finalScore = (int)(startTime - timer) + score;
				GameManager.instance.EndGame();
				endTheGame = true;
			}
		}
		
	}

	public void Reset()
	{
		npcCount = 0;
		enemyCount = 0;
		timer = startTime;
		score = 0;
		candyCount = 5;
		enemiesDefeated = 0;
		finalScore = 0;
	}
}
