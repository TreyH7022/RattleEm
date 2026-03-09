using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    private float moveSpeed;

    public Transform player;
    private Rigidbody2D rb;
    public GameObject enemyPrefab;
    public AudioClip hit;
    public GameObject hitEffect;

    private AudioSource audioSource;
    private Vector2 movement;
    private bool isDead = false;

    void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        
        rb = GetComponent<Rigidbody2D>();

        // Auto-find the player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        audioSource = GetComponent<AudioSource>();
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

    public void TakeHit() {
        if (isDead) return;

        isDead = true;

        if (hit != null && audioSource != null) {
            audioSource.PlayOneShot(hit);
        }

        if(hitEffect != null) {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 0.1f);
    }

}