using TankBattle.Extensions;
using UnityEngine;

namespace TankBattle.TankService.PlayerTank.MoveService
{
    public class TankController
    {
        public TankModel tankModel { get; }
        public TankView tankView { get; }

        private Rigidbody rb;
        private Transform transform;

        public TankController(TankModel _tankModel, TankView tankPrefab)
        {
            tankModel = _tankModel;
            tankView = Object.Instantiate(tankPrefab);
        }
        
        public void Move(Vector2 _moveDirection)
        {
            Vector3 moveDirection = _moveDirection.switchYAndZValues();
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            targetRotation = Quaternion.RotateTowards
            (
            tankView.transform.localRotation,
            targetRotation,
                tankModel.RotateSpeed * Time.fixedDeltaTime
            );

            if (rb == null)
            {
                rb = tankView.getRigidbody();
            }

            rb.MovePosition(rb.position + moveDirection * tankModel.Speed * Time.deltaTime);
            rb.MoveRotation(targetRotation);
        }
        public void Jump()
        {
            if (!rb)
            {
                rb = tankView.GetComponent<Rigidbody>();
            }
            rb.AddForce(Vector3.up * tankModel.JumpForce, ForceMode.Impulse);
        }
    };
}