using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public void MainMenuButton()
    {
        GameManager.Load(GameManager.Scene.MainMenu);
    }
}
