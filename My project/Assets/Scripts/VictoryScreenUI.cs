using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenUI : MonoBehaviour
{

    [SerializeField] private string nomeDoLevelDeJogo;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void QuitGame()
    {
        Debug.Log("🚪 Saindo do jogo...");
        Application.Quit();
    }
}
