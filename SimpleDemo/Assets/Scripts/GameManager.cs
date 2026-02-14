using System;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Constants
    private const string GAME_IS_STARTED = "gameIsStarted";

    // Game state flags
    [SerializeField] private bool gameIsStarted;

    // UI elements
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject gamePanel;

    // Singleton instance for easy access
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        // TODO: Remove this once game over is implemented
        SetGameIsStarted(false);
    }

    // Initialize game state and UI
    private void Start()
    {
        introPanel.SetActive(true);
        gamePanel.SetActive(false);

        if (!PlayerPrefs.HasKey(GAME_IS_STARTED))
        {
            SetGameIsStarted(false);
            Debug.Log("Creating Player Pref for: " + GAME_IS_STARTED);
            return;
        }

        if (PlayerPrefs.GetInt(GAME_IS_STARTED) == 1) LoadGame();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Quit();
        if (Input.GetButtonDown("Restart")) Restart();
    }

    // Start the game
    public void NormalStartGame()
    {
        introPanel.SetActive(false);
        gamePanel.SetActive(true);
        SetGameIsStarted(true);
        AudioManager.Instance.PlayMenuSfx(GameSfx.Start);
    }

    // Load game
    public void LoadGame()
    {
        introPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    // Restart the game
    private void Restart()
    {
        DOTween.KillAll();
        Debug.Log("Restart Game...");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Destroy(gameObject);

        introPanel.SetActive(true);
        gamePanel.SetActive(false);
        SetGameIsStarted(false);
    }

    // Quit the game
    private void Quit()
    {
        DOTween.KillAll();
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void SetGameIsStarted(bool value)
    {
        gameIsStarted = value;
        PlayerPrefs.SetInt(GAME_IS_STARTED, Convert.ToInt32(gameIsStarted));
        Debug.Log("Game Started: " + gameIsStarted);
    }
}