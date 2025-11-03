using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public Image playerIcon;
    public Image heartIcon;
    public Text livesText;

    public void UpdateLife(int currentLives)
    {
        if (livesText != null)
            livesText.text = "x" + currentLives;
    }
}