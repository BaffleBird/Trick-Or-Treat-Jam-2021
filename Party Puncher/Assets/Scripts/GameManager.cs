using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Things to do at after scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            dataSystem.Reset();
        }
    }

    //Set of scene names
    public enum Scenes
    {
        MainMenu,
        GameScene,
        ScoreScene
    }

    //Load function for game scenes
    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }


}