using UnityEngine;

public class MultiShotSpawner : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject multiShotPowerUp;
    public float spawnInterval = 15f; 
    public Vector2 spawnAreaMin = new Vector2(-23, -4);
    public Vector2 spawnAreaMax = new Vector2(23, 4);

    private GameObject currentPowerUp;
    private float timer;

    void Update()
    {

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

        currentPowerUp = Instantiate(multiShotPowerUp, randomPos, Quaternion.identity);

        MultiShotPowerUp msp = currentPowerUp.GetComponent<MultiShotPowerUp>();
        if (msp != null)
        {
            msp.OnCollected += HandlePowerUpCollected;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(
            (spawnAreaMin + spawnAreaMax) / 2f,
            spawnAreaMax - spawnAreaMin
        );
    }
}