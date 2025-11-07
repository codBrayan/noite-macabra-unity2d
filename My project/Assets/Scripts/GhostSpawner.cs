using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnDistance = 3f; // distância além da borda da câmera
    [SerializeField] private Camera mainCamera;

    private float timer;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    private void SpawnGhost()
    {
        if (!ghostPrefab || !mainCamera) return;

        Vector3 spawnPos = GetOffscreenPosition();
        Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// Gera uma posição fora da área visível da câmera.
    /// </summary>
    private Vector3 GetOffscreenPosition()
    {
        // Pega dimensões da câmera
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camCenter = mainCamera.transform.position;

        // Escolhe um dos quatro lados da tela
        int side = Random.Range(0, 4);
        Vector3 spawnPos = camCenter;

        switch (side)
        {
            case 0: // Esquerda
                spawnPos.x -= camWidth / 2 + spawnDistance;
                spawnPos.y += Random.Range(-camHeight / 2, camHeight / 2);
                break;

            case 1: // Direita
                spawnPos.x += camWidth / 2 + spawnDistance;
                spawnPos.y += Random.Range(-camHeight / 2, camHeight / 2);
                break;

            case 2: // Topo
                spawnPos.y += camHeight / 2 + spawnDistance;
                spawnPos.x += Random.Range(-camWidth / 2, camWidth / 2);
                break;

            case 3: // Fundo
                spawnPos.y -= camHeight / 2 + spawnDistance;
                spawnPos.x += Random.Range(-camWidth / 2, camWidth / 2);
                break;
        }

        return spawnPos;
    }

    public void ResetSpawner()
    {
        timer = 0f;
        StopAllCoroutines();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (!mainCamera) return;

        // Pega dimensões da câmera
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camCenter = mainCamera.transform.position;

        // Área visível
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(camCenter, new Vector3(camWidth, camHeight, 0));

        // Área de spawn (fora da tela)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(camCenter, new Vector3(camWidth + spawnDistance * 2, camHeight + spawnDistance * 2, 0));
    }
#endif
}