using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    public Vector3 offset = new Vector3(-3f, 2f, -10f); 
    // 🔥 ini yang bikin kamera "di belakang"

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPosition;
    }
}
