using UnityEngine;
using System.Collections;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private Vector2 areaCenter = Vector2.zero;
    [SerializeField] private Vector2 areaSize = new Vector2(10, 5);

    [Header("Spawn Settings")]
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private LayerMask blockingLayers;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private int maxAttempts = 15;

    [Header("Timing Settings")]
    [SerializeField] private float spawnInterval = 1.0f;   // time between spawns
    [SerializeField] private float spawnDuration = 10.0f;  // total time to keep spawning

    private bool isSpawning;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
            StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;
        float endTime = Time.time + spawnDuration;

        while (Time.time < endTime)
        {
            TrySpawnCharacter();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        Debug.Log("Spawning finished.");
    }

    private void TrySpawnCharacter()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 spawnPos = GetRandomPositionInArea();
            bool hasCollider = Physics2D.OverlapCircle(spawnPos, checkRadius, blockingLayers);

            if (!hasCollider)
            {
                Instantiate(characterPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("Failed to find valid spawn position after multiple attempts.");
    }

    private Vector2 GetRandomPositionInArea()
    {
        float x = Random.Range(areaCenter.x - areaSize.x / 2f, areaCenter.x + areaSize.x / 2f);
        float y = Random.Range(areaCenter.y - areaSize.y / 2f, areaCenter.y + areaSize.y / 2f);
        return new Vector2(x, y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawCube(areaCenter, areaSize);
    }
#endif
}