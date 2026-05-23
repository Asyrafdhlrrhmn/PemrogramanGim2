using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthSlider;

    public PlayerHealth playerHealth;

    void Update()
    {
        if (playerHealth != null)
        {
            healthSlider.value =
                playerHealth.CurrentHealth();
        }
    }
}