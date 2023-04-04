using TankBattle.Services;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Tank.UI
{
    public class HealthUISystem : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image fillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;
        
        private TankModel tankModel;

        private float maxHealth;
        private int tankIndex;
        //private TankController tankController;

        private void Start()
        {
            TankView tankView = gameObject.GetComponent<TankView>();
            TankController tankController = tankView.GetTankController();
            tankModel = tankController.TankModel;
            SetIndexAndMaxHealth();
            SubScribeToHealthUpdates();
        }

        private void SubScribeToHealthUpdates()
        {
            tankModel.HealthChanged += SetHealthUI;
        }

        private void UnSubScribeToHealthUpdates()
        {
            tankModel.HealthChanged -= SetHealthUI;
        }

        private void SetIndexAndMaxHealth()
        {
            SetHealth(tankModel.Health);
        }

        private void OnDestroy()
        {
            UnSubScribeToHealthUpdates();
        }

        private void SetHealth(float health)
        {
            maxHealth = health;
            SetHealthUI(maxHealth);
        }

        private void SetHealthUI(float value)
        {
                healthSlider.value = value;
                fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, value / maxHealth);
        }

        //private void SetHealthUI()
        //{
        //    healthSlider.value = tankController.TankModel.Health;
        //    fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankController.TankModel.Health / maxHealth);
        //}
    }
}
