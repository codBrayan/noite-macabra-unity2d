using UnityEngine;
using UnityEngine.InputSystem; // necessário se usar o novo Input System

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // velocidade ajustável no Inspector
    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        // GetComponent em Awake é seguro para inicializar referências
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D não encontrado no GameObject.", this);
        }
    }

    // FixedUpdate é chamado em passos de física regulares — ideal para manipular Rigidbody
    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    // Método compatível com o novo Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        // read value pega o Vector2 da ação definida no Input System
        movement = context.ReadValue<Vector2>();
    }
}
