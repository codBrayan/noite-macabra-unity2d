using UnityEngine;
using System;

public class KillAllEnemiesPowerUp : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private float itemLifetime = 7f; 

    public event Action OnCollected;
    private bool collected = false;

    void Start()
    {
        Destroy(gameObject, itemLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;

            KillAllEnemies();

            OnCollected?.Invoke();
            OnCollected = null;

            Destroy(gameObject);
        }
    }

    private void KillAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
    }

    private void OnDestroy()
    {
        OnCollected = null;
    }
}