using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public float speed = 8f;

    private Vector3 targetScale;

    void Start()
    {
        targetScale = normalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnHover()
    {
        targetScale = hoverScale;
    }

    public void OnExit()
    {
        targetScale = normalScale;
    }
}
