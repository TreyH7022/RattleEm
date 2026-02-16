using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public float projectileForce = 10f;
    public AudioClip pew;
    public AudioClip hit;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 newPos = transform.position;

        // WASD movement
        if (Keyboard.current.wKey.isPressed)
        {
            newPos.y += speed * Time.deltaTime;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            newPos.y -= speed * Time.deltaTime;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            newPos.x -= speed * Time.deltaTime;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            newPos.x += speed * Time.deltaTime;
        }
        
        // arrow key movement
        if (Keyboard.current.upArrowKey.isPressed)
        {
            newPos.y += speed * Time.deltaTime;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            newPos.y -= speed * Time.deltaTime;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            newPos.x -= speed * Time.deltaTime;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            newPos.x += speed * Time.deltaTime;
        }

        transform.position = newPos;

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

    // plays sound when enemy hits player
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (hit != null)
            {
                audioSource.PlayOneShot(hit);
            }
        }
    }
}