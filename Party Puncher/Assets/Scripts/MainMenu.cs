using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        GameManager.Load(GameManager.Scene.GameScene);
    }

    public void DisplayButton()
    {
        //Text
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
