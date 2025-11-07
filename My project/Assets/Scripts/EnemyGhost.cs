using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 2f;
    private Transform player;

    [Header("Dano")]
    public int damage = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Move em direção ao player
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        // Vira o sprite na direção do jogador
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
}