using TankBattle.Services;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Tank.UI
{
    public class HealthUISystem : MonoBehaviour, IHealth
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image fillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;

        private float maxHealth;

        private void OnEnable()
        {
            EventService.Instance.OnHealthChange += SetHealthUI;
        }

        private void OnDisable()
        {
            EventService.Instance.OnHealthChange -= SetHealthUI;
        }

        public void SetHealth(float _maxHealth)
        {
            maxHealth = _maxHealth;
            SetHealthUI(maxHealth);
        }

        private void SetHealthUI(float value)
        {
            //healthSlider.value = tankController.TankModel.Health;
            //fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankController.TankModel.Health / maxHealth);

            healthSlider.value = value;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, value / maxHealth);
        }
    }
}
