using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 5f;

    public Vector3 offset;

    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition =
            new Vector3(
                target.position.x + offset.x,
                fixedY,
                offset.z
            );

        Vector3 smoothedPosition =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );

        transform.position =
            new Vector3(
                smoothedPosition.x,
                fixedY,
                offset.z
            );
    }
}