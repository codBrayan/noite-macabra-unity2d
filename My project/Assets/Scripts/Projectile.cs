using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private float damage = 15f; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyGhost ghost = other.GetComponent<EnemyGhost>();
            if (ghost != null)
            {
                ghost.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject);
        }
    }
}