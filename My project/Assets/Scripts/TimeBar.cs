using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [Header("Configuração")]
    public float runDuration = 30f;     // duração total da run (em segundos)
    public Image fillImage;             // referência à barra
    private float currentTime;
    private bool isRunning = false;

    public float RemainingTime => currentTime;
    public bool IsRunning => isRunning;

    void Start()
    {
        currentTime = runDuration;
        fillImage.fillAmount = 1f;
        StartTimer();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0);
        fillImage.fillAmount = currentTime / runDuration;

        if (currentTime <= 0)
        {
            isRunning = false;
            Debug.Log("⏱️ Tempo acabou!");
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void SetRemainingTime(float value)
    {
        currentTime = Mathf.Clamp(value, 0, runDuration);
        fillImage.fillAmount = currentTime / runDuration;
    }
}