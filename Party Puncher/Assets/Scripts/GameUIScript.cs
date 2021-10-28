using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI candyCount;
	[SerializeField] TextMeshProUGUI npcCount;
	[SerializeField] TextMeshProUGUI enemyCount;
	[SerializeField] TextMeshProUGUI timer;
	[SerializeField] TextMeshProUGUI score;

	DataSystem data;

	private void Start()
	{
		data = GameManager.instance.dataSystem;
	}

	private void Update()
	{
		candyCount.text =  data.candyCount.ToString();
		npcCount.text = "Partiers: " + data.npcCount.ToString();
		enemyCount.text = "Monsters: " + data.enemyCount.ToString();
		score.text = "Score: " + data.score.ToString();

		timer.text = ((int)data.timer).ToString();
	}
}
