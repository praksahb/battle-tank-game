using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.PlayerTank
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField] private InputSystem.InputReader input;
        private TankController tankController;


        private void Start()
        {
            tankController = PlayerService.Instance.GetTankController();
            if (tankController == null)
            {
                Debug.Log("ERROR");
            }
        }

        private void OnEnable()
        {
            input.FireEventPressed += HandleFirePressed;
            input.FireEventReleased += HandleFireReleased;
        }

        private void OnDisable()
        {
            input.FireEventPressed -= HandleFirePressed;
            input.FireEventReleased -= HandleFireReleased;
        }
        private void HandleFireReleased()
        {
            BulletProjectile.isFirePressed = false;
            tankController.Fire();
        }

        private void HandleFirePressed()
        {
            BulletProjectile.isFirePressed = true;
        }
    }
}
