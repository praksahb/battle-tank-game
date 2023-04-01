using TankBattle.Extensions;
using TankBattle.Tank.Bullets;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle.Tank
{
    public class TankController
    {
        public TankModel TankModel { get; }
        public TankView TankView { get; }

        private Rigidbody rb;

        private float currentLaunchForce;
        public float CurrentLaunchForce { get => currentLaunchForce; set => currentLaunchForce = value; }

        public float ChargeSpeed { get; }
        public bool IsFired { get; set; }

        public TankController(TankModel tankModel, TankView tankPrefab, Vector3 spawnPosition)
        {
            TankModel = tankModel;
            TankView = Object.Instantiate(tankPrefab, spawnPosition, Quaternion.identity);
            TankView.SetColorOnAllRenderers(TankModel.Color);
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
        public void KillTank()
        {
            ChangeHealth(TankModel.Health);
            TankView.SetHealthUI();
            OnDeath();
        }

        public void TakeDamage(Vector3 impactPosition, float _explosionRadius, float _MaxDamage)
        {
            float amount = CalculateDamage(impactPosition, _explosionRadius, _MaxDamage);
            ChangeHealth(amount);
            TankView.SetHealthUI();
            if (TankModel.Health <= 0f && !TankModel.IsDead)
            {
                OnDeath();
            }
        }
        private float CalculateDamage(Vector3 impactPosition, float ShellModel_explosionRadius, float ShellModel_MaxDamage)
        {
            float explosionDistance = (TankView.transform.position - impactPosition).sqrMagnitude;

            float relativeDistance = (ShellModel_explosionRadius - explosionDistance) / ShellModel_explosionRadius;

            float damage = relativeDistance * ShellModel_MaxDamage;
            damage = Mathf.Max(damage, 0f);
            return damage;
        }
        private void ChangeHealth(float dec_val)
        {
            TankModel.Health -= dec_val;
        }

        private void OnDeath()
        {
            TankModel.IsDead = true;
            TankView.InstantiateOnDeath();

            if(TankModel.TankTypes == TankType.Player)
            {
                PlayerService.Instance.InvokePlayerDeathEvent();
            }
            else if(TankModel.TankTypes == TankType.Enemy && !PlayerService.Instance.GetTankController().TankModel.IsDead)
            {
                EnemyService.Instance.ReduceEnemyList(this);
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
            
                ShellService.Instance.LaunchBullet(fireTransform, bulletSpeed, TankModel.TankTypes);

                if(TankModel.TankTypes == TankType.Player)
                {
                    PlayerService.Instance.IncrementBulletsFiredScore();
                }

                TankView.PlayFiredSound();
                currentLaunchForce = TankModel.MinLaunchForce;
            } else
            {
                Debug.LogError("Fire transform is null");
            }
        }
    }
}