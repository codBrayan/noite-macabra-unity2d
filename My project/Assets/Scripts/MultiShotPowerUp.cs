using UnityEngine;
using System;

public class MultiShotPowerUp : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int projectileCount = 8;
    [SerializeField] private float powerUpDuration = 10f;
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
            GunController gun = collision.GetComponentInChildren<GunController>();

            if (gun != null)
            {
                gun.ActivateMultiShot(projectileCount, powerUpDuration);
            }

            collected = true;

            OnCollected?.Invoke();
            OnCollected = null; 
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnCollected = null;
    }
}