using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

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
    }
}
