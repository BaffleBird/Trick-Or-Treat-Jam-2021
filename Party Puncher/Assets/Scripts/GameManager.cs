using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Prevent non-singleton constructor use
    protected GameManager() { }

    // Add whatever code to the class you need as you normally would
    public string MyTestString = "Hello world!";

    // Set of scene names
    public enum Scene
    {
        MainMenuScene,
        GameScene
    }

    // Load function for game scenes
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void getNumber()
    {
        return;
    }
}


public class Testing : MonoBehaviour
{
    // test
    private void OnEnable()
    {
        Debug.Log(GameManager.Instance.MyTestString);
    }
}