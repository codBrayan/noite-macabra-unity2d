using UnityEngine;
using System.Collections;

public class GameRunManager : MonoBehaviour
{
    public static GameRunManager Instance;

    [Header("Referências")]
    [SerializeField] private TimerBar timerBar;
    [SerializeField] private GhostSpawner ghostSpawner;
    [SerializeField] private GameObject victoryScreen;

    private bool isRestarting = false;
    private bool victoryShown = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (timerBar != null)
            timerBar.OnTimerEnd += HandleTimerEnd;
    }

    private void HandleTimerEnd()
    {
        Debug.Log("👻 Tempo acabou — parando o spawn de inimigos!");
        if (ghostSpawner != null)
            ghostSpawner.enabled = false;
    }

    public void OnPlayerHit()
    {
        if (isRestarting) return;
        isRestarting = true;

        Debug.Log("💢 Player atingido — pausando run...");

        // Salva tempo atual e pausa timer
        if (timerBar)
        {
            
            timerBar.StopTimer();

            timerBar.AddTime(10f);
        }

        // Para o spawn e remove inimigos
        if (ghostSpawner) ghostSpawner.StopAllCoroutines();
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        // Espera e retoma
        StartCoroutine(RestartRun());
    }

    void Update()
    {
        if (victoryShown || isRestarting) return; // impede checagem durante reinício

        bool timeOver = !timerBar.IsRunning;
        bool noEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;

        if (timeOver && noEnemiesLeft)
        {
            ShowVictory();
        }
    }
    private void ShowVictory()
    {
        victoryShown = true;
        Time.timeScale = 0f; // pausa o jogo
        victoryScreen.SetActive(true);
        Debug.Log("🎉 Vitória! Todos os inimigos derrotados e o tempo acabou!");
    }

    private IEnumerator RestartRun()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("🔁 Retomando run...");

        if (timerBar)
        {
            timerBar.StartTimer();
        }

        if (ghostSpawner)
        {
            ghostSpawner.ResetSpawner();
        }

        isRestarting = false;
    }
}