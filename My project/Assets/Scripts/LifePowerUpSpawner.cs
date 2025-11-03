using UnityEngine;

public class LifePowerUpSpawner : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject lifePowerUp;
    public float spawnInterval = 10f;
    public Vector2 spawnAreaMin = new Vector2(-23, -4);
    public Vector2 spawnAreaMax = new Vector2(23, 4);

    private GameObject currentPowerUp;
    private float timer;

    void Update()
    {
        // Garante que o objeto não foi destruído
        if (IsObjectAlive(currentPowerUp))
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        currentPowerUp = Instantiate(lifePowerUp, randomPos, Quaternion.identity);

        LifePowerUp lp = currentPowerUp.GetComponent<LifePowerUp>();
        if (lp != null)
        {
            lp.OnCollected += HandlePowerUpCollected;
        }
    }

    void HandlePowerUpCollected()
    {
        currentPowerUp = null;
    }

    bool IsObjectAlive(GameObject obj)
    {
        return obj != null && !obj.Equals(null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            (spawnAreaMin + spawnAreaMax) / 2f,
            spawnAreaMax - spawnAreaMin
        );
    }
}