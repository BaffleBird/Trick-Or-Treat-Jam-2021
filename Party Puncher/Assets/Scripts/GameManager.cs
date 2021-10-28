using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Other stuff
    bool pause = false;
    float currentTimescale = 1f;
    bool endGame = false;
    float fadeOutTime = 0;
    Image FadeOutScreen;

    //Systems Instantiations
    public DataSystem dataSystem = new DataSystem();

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	private void Update()
	{
        dataSystem.Update();
        if(Keyboard.current.pKey.wasPressedThisFrame)
		{
            pause = !pause;
		}

        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = currentTimescale;

        if (endGame)
		{
            fadeOutTime -= Time.unscaledDeltaTime;
            FadeOutScreen.color = Color.Lerp(FadeOutScreen.color, new Color(0, 0, 0, 1), 0.02f);
            currentTimescale = Mathf.Lerp(currentTimescale, 0, 0.02f);
            Time.timeScale = currentTimescale;
            if (fadeOutTime <= 0)
                Load(Scenes.MainMenuScene);
        }
        
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Things to do at after scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pause = false;
        endGame = false;
        fadeOutTime = 0;
        currentTimescale = 1;
        Time.timeScale = currentTimescale;
        if (scene.name == "GameScene")
        {
            dataSystem.Reset();
            FadeOutScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Image>();
        } 
    }

    //Set of scene names
    public enum Scenes
    {
        MainMenuScene,
        GameScene
    }

    //Load function for game scenes
    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    //End Game
    public void EndGame()
	{
        if(SceneManager.GetActiveScene().name == "GameScene")
		{
            fadeOutTime = 5;
            endGame = true;
		}
	}

    
}
