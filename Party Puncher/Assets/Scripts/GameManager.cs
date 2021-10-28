using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Set of scene names
    public enum Scene
    {
        MainMenu,
        GameScene,
        ScoreScene
    }

    // Load function for game scenes
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
