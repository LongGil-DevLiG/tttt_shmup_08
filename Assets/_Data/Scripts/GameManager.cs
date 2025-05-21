// GameManager.cs - Quản lý game state

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private int currentLives;
    [SerializeField] private int score = 0;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip victorySound;

    private bool isGameOver = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize game state
        currentLives = startingLives;
        score = 0;
        isGameOver = false;

        // Hide UI panels
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    private void Start()
    {
        UpdateUI();
    }

    // Cập nhật UI score và lives
    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;
        score += points;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Play game over sound
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }

        // Show game over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + score;
            }
        }

        // Pause the game or slow it down
        Time.timeScale = 0.3f;
    }

    public void GameComplete()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Play victory sound
        if (victorySound != null)
        {
            AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        }

        // Show victory UI
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + score;
            }
        }

        // Pause the game
        Time.timeScale = 0.3f;
    }

    public void RestartGame()
    {
        // Reset game state
        currentLives = startingLives;
        score = 0;
        isGameOver = false;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Hide UI panels
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);

        // Reset game time scale to normal
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
