using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    AudioSource audioSource;

    string currentScene = "MainMenu";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Initial menu music can be loaded at start
        // Use update to check scene for corresponding scene music
        audioSource.clip = Resources.Load<AudioClip>("Music/Map");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Initial play for first time Main Menu scene
        audioSource.Play();
    }

    void Update()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            currentScene = SceneManager.GetActiveScene().name;
            SwitchAudioClip(SceneManager.GetActiveScene().name);
        }
    }

    // Assign AudioClip to specified scene, called via Update whenever scene change is detected
    public void SwitchAudioClip (string targetScene)
    {
        if (targetScene == "TestMusic1")
        {
            audioSource.clip = Resources.Load<AudioClip>("Music/Map");
        }
        else if (targetScene == "TestMusic2")
        {
            audioSource.clip = Resources.Load<AudioClip>("Music/Map");
        }
        else if (targetScene == "GameScene")
        {
            audioSource.clip = Resources.Load<AudioClip>("Music/Constant Moderato");
            // Serves as 'Play On Awake' for transition from Menu music to Game music
            audioSource.Play();
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    // Set of scene names
    public enum Scene
    {
        MainMenu,
        GameScene,
        TestMusic1,
        TestMusic2
    }

    // Load function for game scenes
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
