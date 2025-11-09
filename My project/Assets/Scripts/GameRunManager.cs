using UnityEngine;
using System.Collections;

public class GameRunManager : MonoBehaviour
{
    public static GameRunManager Instance;

    [Header("Referências")]
    public TimerBar timerBar;
    public GhostSpawner ghostSpawner;

    private bool isRestarting = false;
    private float savedTime; // tempo salvo no momento da pausa

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPlayerHit()
    {
        if (isRestarting) return;
        isRestarting = true;

        Debug.Log("💢 Player atingido — pausando run...");

        // Salva tempo atual e pausa timer
        if (timerBar)
        {
            savedTime = timerBar.RemainingTime;
            timerBar.StopTimer();
        }

        // Para o spawn e remove inimigos
        if (ghostSpawner) ghostSpawner.StopAllCoroutines();
        //foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //    Destroy(enemy);

        // Espera e retoma
        StartCoroutine(RestartRun());
    }

    private IEnumerator RestartRun()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("🔁 Retomando run...");

        if (timerBar)
        {
            timerBar.SetRemainingTime(savedTime);
            timerBar.StartTimer();
        }

        if (ghostSpawner)
        {
            ghostSpawner.ResetSpawner();
        }

        isRestarting = false;
    }
}