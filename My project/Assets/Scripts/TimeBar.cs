using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerBar : MonoBehaviour
{
    [Header("Configuração")]
    public float runDuration = 30f;     // duração total da run (em segundos)
    public Image fillImage;             // referência à barra
    private float currentTime;
    private bool isRunning = false;
    private bool hasEnded = false;      // novo: controla se o tempo terminou

    public float RemainingTime => currentTime;
    public bool IsRunning => isRunning;

    public event Action OnTimerEnd;

    void Start()
    {
        currentTime = runDuration;
        fillImage.fillAmount = 1f;
        StartTimer();
    }

    void Update()
    {
        if (!isRunning || hasEnded) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0);
        fillImage.fillAmount = currentTime / runDuration;

        if (currentTime <= 0 && !hasEnded)
        {
            hasEnded = true;
            isRunning = false;
            Debug.Log("Tempo acabou!");
            OnTimerEnd?.Invoke();
        }
    }

    public void StartTimer()
    {
        if (hasEnded) return; // impede reinício após terminar
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void AddTime(float amount)
    {
        currentTime = Mathf.Min(currentTime + amount, runDuration);
        fillImage.fillAmount = currentTime / runDuration;
    }

    public void SetRemainingTime(float value)
    {
        currentTime = Mathf.Clamp(value, 0, runDuration);
        fillImage.fillAmount = currentTime / runDuration;
    }
}

