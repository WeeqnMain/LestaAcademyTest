using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerComponents
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Slider healthSlider;

        private void Awake()
        {
            playerHealth.HealthChanged += OnHealthChanged; 
            healthSlider.value = healthSlider.maxValue;
        }

        private void OnHealthChanged(int value)
        {
            healthSlider.value = value / (float)playerHealth.MaxHealth;
        }
    }
}
