using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;
    public bool clampToBounds = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth  = cam.orthographicSize * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        if (clampToBounds)
        {
            smoothedPos.x = Mathf.Clamp(smoothedPos.x,
                minBounds.x + camHalfWidth,
                maxBounds.x - camHalfWidth);

            smoothedPos.y = Mathf.Clamp(smoothedPos.y,
                minBounds.y + camHalfHeight,
                maxBounds.y - camHalfHeight);
        }

        transform.position = smoothedPos;
    }
}