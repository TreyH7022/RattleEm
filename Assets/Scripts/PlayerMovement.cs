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

    private AudioSource audioSource;
    private bool isDead = false;
    private Animator animator;
    private Vector2 movement;

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

        transform.position = newPos;

        // Run animation
        animator.SetFloat("Speed", movement.magnitude);
 
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

    // plays sound when enemy hits player
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isDead)
        {
            isDead = true;

            if (hit != null)
            {
                audioSource.PlayOneShot(hit);
            }

            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            StartCoroutine(GameOverAfterDelay());

        }
    }

    IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

        gameOver.SetActive(true);

        Time.timeScale = 0f; 
    }
}