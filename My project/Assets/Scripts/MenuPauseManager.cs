using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuPauseManager : MonoBehaviour
{

    [SerializeField] private GameObject menuPausaUI; 
    [SerializeField] private string nomeDoLevelDeJogo;

    private bool jogoPausado = false;

    void Update()
    {

       if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (jogoPausado)
            {
                ContinuarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    public void ContinuarJogo()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;
        Debug.Log("Continuar");

    }

    private void PausarJogo()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; 
        jogoPausado = true;
        Debug.Log("Pausar");

    }

    public void VoltarMenuPrincipal()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
