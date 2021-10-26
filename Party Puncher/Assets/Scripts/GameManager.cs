using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Unity Singleton
	#region Singleton
    //Basically only one of this class can exist across scenes. If you need something from it, call GameManager.instance.whatever
	private static GameManager _instance;
    public static GameManager instance => _instance; // Global Reference

    private void Awake()
    {
        //Destroy duplicates
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //Preserve across scenes
        DontDestroyOnLoad(gameObject);
    }
    #endregion

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
