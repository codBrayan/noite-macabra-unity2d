using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public Image playerIcon;
    public TextMeshProUGUI livesText;

    public void UpdateLife(int currentLives)
    {
        if (livesText != null)
            livesText.text = "x" + currentLives;
    }
}