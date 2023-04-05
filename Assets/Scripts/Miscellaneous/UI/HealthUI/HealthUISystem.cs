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

        private void Start()
        {
            TankView tankView = gameObject.GetComponent<TankView>();
            TankController tankController = tankView.GetTankController();
            tankModel = tankController.TankModel;
            SetInitialHealth();
            SubscribeToHealthUpdates();
        }

        private void SubscribeToHealthUpdates()
        {
            tankModel.HealthChanged += SetHealthUI;
        }

        private void UnSubscribeToHealthUpdates()
        {
            tankModel.HealthChanged -= SetHealthUI;
        }

        private void OnDestroy()
        {
            UnSubscribeToHealthUpdates();
        }

        private void SetInitialHealth()
        {
            maxHealth = tankModel.Health;
            SetHealthUI();
        }

        //private void SetHealthUI(float value)
        //{
        //        healthSlider.value = value;
        //        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, value / maxHealth);
        //}
        private void SetHealthUI()
        {
            healthSlider.value = tankModel.Health;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankModel.Health / maxHealth);
        }
    }
}
