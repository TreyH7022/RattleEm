using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public float projectileForce = 10f;
    public AudioClip pew;
    public AudioClip hit;
    public GameObject hitEffectPrefab;
    public GameObject gameOver;
    public float gameOverDelay = 1f;
    public GameManager gameManager;
    public bool clampToBounds = true;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private AudioSource audioSource;
    private bool isDead = false;
    private Animator animator;
    private Vector2 movement;
    private bool facingRight = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Move the player using the Input System movement vector
        Vector3 newPos = transform.position;

        newPos.x += movement.x * speed * Time.deltaTime;
        newPos.y += movement.y * speed * Time.deltaTime;

        if (clampToBounds) 
        {
            newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
            newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);
        }
        transform.position = newPos;

        // Run animation
        animator.SetFloat("Speed", movement.magnitude);

        // Flip sprite based on horizontal movement
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
 
        // Spacebar action
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {

            audioSource.PlayOneShot(pew);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;

            Vector2 direction = (mousePos - transform.position).normalized;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        
            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }

        // Mouse click action 
                if (Mouse.current.leftButton.wasPressedThisFrame) {

            audioSource.PlayOneShot(pew);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;

            Vector2 direction = (mousePos - transform.position).normalized;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        
            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Movement: " + movement);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // flip horizontally
        transform.localScale = scale;
    }

    // plays sound when enemy hits player
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isDead)
        {
            isDead = true;

            AudioSource.PlayClipAtPoint(hit, transform.position);

            if (hitEffectPrefab != null)
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);

            if (gameManager != null) {
                gameManager.PlayerDied();
            }
        }
    }

    IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

        gameOver.SetActive(true);

        Time.timeScale = 0f; 
    }
}