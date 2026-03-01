using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject foodPrefab;      // Prefab of food (with ItemPickup)
    public Transform[] spawnPoints;    // Array of spawn locations
    public float respawnTime = 10f;    // Time before respawn

    public void SpawnFoodAtRandom()
    {
        if (spawnPoints.Length == 0 || foodPrefab == null) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(foodPrefab, point.position, Quaternion.identity);
    }

    // Call this to respawn food after it was picked up
    public void RespawnFood()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnFoodAtRandom();
    }
}