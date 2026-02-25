using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    private Rigidbody2D rb;
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;

    private float spawnTimer;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnTimer = spawnInterval;

        // Auto-find the player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Direction from enemy to player
        Vector2 direction = (player.position - transform.position);

        // Normalize so speed is consistent
        movement = direction.normalized;

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}