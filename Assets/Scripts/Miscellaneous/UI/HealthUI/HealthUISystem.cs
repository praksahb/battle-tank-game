using System.Collections;
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

        private float maxHealth;
        private int tankIndex;
        private TankController tankController;

        private void OnEnable()
        {
            GetTankController();
            EventService.Instance.OnHealthChange += SetHealthUI;
        }

        private void GetTankController()
        {
            TankView tankView = gameObject.GetComponent<TankView>();
            tankController = tankView.GetTankController();
            SetHealth();
            //tankIndex = tankController.TankModel.TankIndex;
        }

        private void OnDisable()
        {
            EventService.Instance.OnHealthChange -= SetHealthUI;
        }

        public void SetHealth()
        {
            maxHealth = tankController.TankModel.Health;
        }

        //private void SetHealthUI(float value, int index)
        //{
        //    if(tankIndex == index)
        //    {
        //        healthSlider.value = value;
        //        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, value / maxHealth);
        //    }
        //}

        private void SetHealthUI()
        {
            healthSlider.value = tankController.TankModel.Health;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankController.TankModel.Health / maxHealth);
        }
    }
}
