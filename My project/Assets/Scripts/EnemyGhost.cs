using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 2f;
    private Transform player;

    [Header("Dano")]
    public int damage = 1;

    [Header("Vida")]
    [SerializeField] private float maxHealth = 30f;
    private float currentHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        Vector2 direction = player.position - transform.position;
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var life = collision.GetComponent<CharacterLifeSystem>();
            life.TakeDamage(damage);

            if (GameRunManager.Instance != null)
                GameRunManager.Instance.OnPlayerHit();

            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        //Debug.Log($"Fantasma tomou {damageAmount} de dano. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Futuramente, pode tocar uma animação de morte ou dropar um item.
        Destroy(gameObject);
    }
}