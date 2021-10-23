using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Initial menu music can be loaded at start
        // Use awake to check scene for corresponding scene music
        audioSource.clip = Resources.Load<AudioClip>("Music/Map");
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        MainMenuScene,
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
