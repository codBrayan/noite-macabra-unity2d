using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLifeSystem : MonoBehaviour
{
    [Header("Configuração de Vida")]
    public int startLives = 3;     
    public int currentLives;       
    public LifeUI lifeUI;

    public AudioSource source;
    public AudioClip damageClip;

    void Start()
    {
        currentLives = startLives;
        UpdateLifeUI();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        source.PlayOneShot(damageClip);
        currentLives = Mathf.Max(currentLives, 0);

        Debug.Log($"💥 Player tomou {damage} de dano! Vidas restantes: {currentLives}");
        UpdateLifeUI();

        if (currentLives <= 0)
        {
            Die();
        }
    }

    public void AddLife(int amount)
    {
        currentLives += amount;
        Debug.Log($"💖 Player ganhou {amount} vida(s)! Total: {currentLives}");
        UpdateLifeUI();
    }

    private void UpdateLifeUI()
    {
        if (lifeUI != null)
            lifeUI.UpdateLife(currentLives);
    }

private void Die()
{
    Debug.Log("💀 Game Over!");

    var input = GetComponent<PlayerInput>();
    if (input != null)
        input.enabled = false;

    var movement = GetComponent<CharacterMovement>();
    if (movement != null)
        movement.enabled = false;

    if (GameOverManager.Instance != null)
        GameOverManager.Instance.ShowGameOver();
}
}
