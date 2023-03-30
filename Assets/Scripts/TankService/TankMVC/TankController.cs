using TankBattle.Extensions;
using TankBattle.Tank.Bullets;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle.Tank
{
    public class TankController
    {
        public TankModel TankModel { get; }
        public TankView TankView { get; }

        private Rigidbody rb;
        private bool isDead;

        private float currentLaunchForce;
        public float CurrentLaunchForce { get => currentLaunchForce; set => currentLaunchForce = value; }

        public float ChargeSpeed { get; }
        public bool IsFired { get; set; }

        public TankController(TankModel tankModel, TankView tankPrefab, Vector3 spawnPosition)
        {
            TankModel = tankModel;
            TankView = Object.Instantiate(tankPrefab, spawnPosition, Quaternion.identity);
            TankView.SetColorOnAllRenderers(TankModel.Color);
            isDead = false;
            ChargeSpeed = (TankModel.MaxLaunchForce - TankModel.MinLaunchForce) / TankModel.MaxChargeTime;
        }

        //Movement-related logic
        public void MoveRotate(Vector2 _moveDirection)
        {
            Vector3 directionVector = _moveDirection.switchYAndZValues();
            Move(directionVector);
            Rotate(directionVector);
        }
        private void Move(Vector3 moveDirection)
        {
            if (!rb)
            {
                rb = TankView.GetRigidbody();
            }
            rb.MovePosition(rb.position + moveDirection * TankModel.Speed * Time.deltaTime);
        }
        private void Rotate(Vector3 rotateDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            targetRotation = Quaternion.RotateTowards
            (
                TankView.transform.localRotation,
                targetRotation,
                TankModel.RotateSpeed * Time.fixedDeltaTime
            );
            if (!rb)
            {
                rb = TankView.GetRigidbody();
            }
            rb.MoveRotation(targetRotation);
        }
        public void Jump()
        {
            if (!rb)
            {
                rb = TankView.GetComponent<Rigidbody>();
            }
            rb.AddForce(Vector3.up * TankModel.JumpForce * Time.deltaTime, ForceMode.Impulse);
        }

        // health related logic
        public void TakeDamage(float amount)
        {
            TankModel.Health -= amount;
            TankView.SetHealthUI();
            if (TankModel.Health <= 0f && !isDead)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            isDead = true;
            TankView.InstantiateOnDeath();

            if(TankModel.TankTypes == TankType.Player)
            {
                PlayerService.Instance.InvokePlayerDeathEvent();
            }
            else if(TankModel.TankTypes == TankType.Enemy && !PlayerService.Instance.GetTankController().isDead)
            {
                PlayerService.Instance.IncrementEnemyKilledScore();
            }
        }

        // Shooting Related

        public void Fire()
        {
            IsFired = true;
            Transform fireTransform = TankView.GetFireTransform();
            if(fireTransform != null)
            {
                Vector3 bulletSpeed = currentLaunchForce * fireTransform.forward;
            
                CreateShellService.Instance.LaunchBullet(fireTransform, bulletSpeed, TankModel.TankTypes);

                if(TankModel.TankTypes == TankType.Player)
                {
                    PlayerService.Instance.IncrementBulletsFiredScore();
                }

                TankView.PlayFiredSound();
                currentLaunchForce = TankModel.MinLaunchForce;
            }
            Debug.LogError("Fire transform is null");
        }
    }
}