using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnDistance = 12f;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // Random direction
        Vector2 direction = Random.insideUnitCircle.normalized;

        // Spawn away from the player
        Vector2 spawnPos = (Vector2)player.position + direction * spawnDistance;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}