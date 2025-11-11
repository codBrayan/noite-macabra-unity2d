using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Importante para Corrotinas

public class GunController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform firePoint; 
    
    [Header("Configuração de Disparo")]
    [SerializeField] private float fireRate = 5f; 
    
    // Esta variável agora é privada e controlada pela corrotina
    private int numberOfProjectiles = 1; 

    private bool isFiring = false;
    private float nextFireTime = 0f;
    private Coroutine powerUpCoroutine;

    void Update()
    { 
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (isFiring && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1f / fireRate);
            Shoot();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) isFiring = true;
        if (context.canceled) isFiring = false;
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        
        if (numberOfProjectiles <= 1)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            float angleStep = 360f / numberOfProjectiles;
            
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float currentAngle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
                Instantiate(projectilePrefab, firePoint.position, rotation);
            }
        }
    }
    
    public void ActivateMultiShot(int projectiles, float duration)
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(MultiShotRoutine(projectiles, duration));
    }
    private IEnumerator MultiShotRoutine(int projectiles, float duration)
    {
        numberOfProjectiles = projectiles;
        
        yield return new WaitForSeconds(duration);
        
        numberOfProjectiles = 1;
        powerUpCoroutine = null;
    }
}