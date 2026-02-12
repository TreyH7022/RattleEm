using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public float projectileForce = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPos = transform.position;

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

        transform.position = newPos;

        if (Keyboard.current.spaceKey.wasPressedThisFrame) {

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
}
