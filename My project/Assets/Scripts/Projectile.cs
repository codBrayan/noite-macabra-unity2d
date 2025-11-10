using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    // A MÁGICA ACONTECE AQUI
    void OnTriggerEnter2D(Collider2D other)
    {
        // Se acertamos um inimigo...
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destrói o inimigo
            Destroy(gameObject);       // Destrói a si mesmo (o projétil)
        }
        
        // Se acertamos qualquer outra coisa (que não seja o Player,
        // mas isso já é garantido pela Layer Matrix)...
        // Usamos "Default" para paredes/tilemap.
        else if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject); // Destrói apenas a si mesmo
        }
        
        // Se acertar o Player ou outro Projétil, não faz nada.
    }
}