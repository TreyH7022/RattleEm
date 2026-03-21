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
    private bool facingRight = true;
    private Animator animator;

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

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Direction from enemy to player
        Vector2 direction = (player.position - transform.position);

        // Normalize so speed is consistent
        movement = direction.normalized;

        // Flip the enemy sprite
        FlipEnemy(direction.x);
    }

    void FlipEnemy(float horizontalDirection)
    {
        if (horizontalDirection > 0 && !facingRight)
            Flip();
        else if (horizontalDirection < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // flip horizontally
        transform.localScale = scale;
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