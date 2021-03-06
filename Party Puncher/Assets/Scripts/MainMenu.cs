using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject displayPopup;

    public void Start()
    {
        // Hide settings popup initially
        displayPopup.transform.localScale = Vector2.zero;
    }

    public void StartButton()
    {
        GameManager.Load(GameManager.Scenes.GameScene);
    }

    public void SettingsOpen()
    {
        displayPopup.transform.LeanScale(Vector2.one, 0.5f);
    }

    public void SettingsClose()
    {
        displayPopup.transform.LeanScale(Vector2.zero, 0.5f);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    // Test scene for music persistence; delete later
    public void MusicButton()
    {
        AudioManager.Instance.Play("Punch");
    }
}
