using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private Sprite somLigadoSprite;
    [SerializeField] private Sprite somDesligadoSPrite;
    [SerializeField] private Image muteImage;

    public void LigarDesligarSom()
    {
        estadoSom = !estadoSom;
        soundtrack.enabled = estadoSom;

        if (estadoSom)
        {
            muteImage.sprite = somLigadoSprite;
        }
        else
        {
            muteImage.sprite = somDesligadoSPrite;
        }
    }
}
