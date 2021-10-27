using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject displayPopup;

    public void Start()
    {
        displayPopup.transform.localScale = Vector2.zero;
    }

    public void StartButton()
    {
        GameManager.Load(GameManager.Scenes.GameScene);
    }

    public void DisplayOpen()
    {
        displayPopup.transform.LeanScale(Vector2.one, 0.5f);
    }

    public void DisplayClose()
    {
        displayPopup.transform.LeanScale(Vector2.zero, 0.5f);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
