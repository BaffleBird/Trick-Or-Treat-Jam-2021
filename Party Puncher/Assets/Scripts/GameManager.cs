using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Set of scene names
    public enum Scene
    {
        MainMenuScene,
        GameScene
    }

    //Load function for game scenes
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
