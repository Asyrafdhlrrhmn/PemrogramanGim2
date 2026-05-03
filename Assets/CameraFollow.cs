using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minX = 0f;
    public float maxX = 20f; // batas kanan

    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = new Vector3(
            clampedX,
            0,
            -10
        );
    }
}
