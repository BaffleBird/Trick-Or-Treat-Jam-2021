using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Menu navigation between two test scenes
    public void Scene1Button()
    {
        GameManager.Load(GameManager.Scene.TestMusic1);
    }

    public void Scene2Button()
    {
        GameManager.Load(GameManager.Scene.TestMusic2);
    }

    // Music implementation
    public void PlayMusic()
    {
        GameManager.Instance.PlaySound();
    }

    public void PauseMusic()
    {
        GameManager.Instance.PauseSound();
    }
}
