using UnityEngine;

// Este script cuida APENAS de atualizar o Animator
// com a direção da mira (mouse).
public class PlayerAim : MonoBehaviour
{
    private Animator playerAnimator;
    private Camera mainCamera; 

    void Start()
    {
        // Pega o Animator do próprio objeto
        playerAnimator = GetComponent<Animator>();
        mainCamera = Camera.main; // Armazena o cache da câmera
    }

    void Update()
    {
        if (playerAnimator == null || mainCamera == null) return;

        // 1. Pega a posição do mouse
        Vector2 mousePosition = Input.mousePosition;

        // 2. Converte a posição do *Player* para a tela
        // (usamos transform.position, que é a posição do Player)
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(transform.position);

        // 3. Calcula o vetor de direção
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        
        // 4. Normaliza o vetor (valores de -1 a 1)
        Vector2 aimDirection = offset.normalized;

        // 5. Envia a direção para o Animator
        // (Exatamente o que o GunController estava fazendo antes)
        playerAnimator.SetFloat("InputX", aimDirection.x);
        playerAnimator.SetFloat("InputY", aimDirection.y);
        playerAnimator.SetFloat("LastInputX", aimDirection.x);
        playerAnimator.SetFloat("LastInputY", aimDirection.y);
    }
}