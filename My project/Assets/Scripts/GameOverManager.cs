using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;     
    public Button retryButton;        
    public Button exitButton;         

    private static GameOverManager instance;
    public static GameOverManager Instance => instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(RestartGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOver()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Restaura o tempo ao normal

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
