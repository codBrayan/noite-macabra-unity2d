using UnityEngine;
using System;

public class LifePowerUp : MonoBehaviour
{
    public int lifeValue = 1;
    public event Action OnCollected;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return; // evita execução duplicada

        if (collision.CompareTag("Player"))
        {
            CharacterLifeSystem lifeSystem = collision.GetComponent<CharacterLifeSystem>();
            if (lifeSystem == null)
                lifeSystem = collision.GetComponentInParent<CharacterLifeSystem>();

            if (lifeSystem != null)
            {
                lifeSystem.AddLife(lifeValue);
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