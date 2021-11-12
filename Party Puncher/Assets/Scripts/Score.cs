using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCount;
    [SerializeField] TextMeshProUGUI timeLasted;
    [SerializeField] TextMeshProUGUI finalScore;

	private void Start()
	{
        killCount.text = GameManager.instance.dataSystem.enemiesDefeated.ToString();
        int s = (int)(GameManager.instance.dataSystem.startTime - GameManager.instance.dataSystem.timer);
        timeLasted.text = s.ToString() + "s";
        finalScore.text = GameManager.instance.dataSystem.finalScore.ToString();

    }

	public void MainMenuButton()
    {
        GameManager.Load(GameManager.Scenes.MainMenu);
    }
}
