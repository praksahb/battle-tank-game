using TankBattle.Extensions;
using TankBattle.Services;
using TankBattle.Tank.Bullets;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using TankBattle.Tank.UI;
using UnityEngine;

namespace TankBattle.Tank
{
    public class TankController
    {
        public TankModel TankModel { get; }
        public TankView TankView { get; }

        private float currentLaunchForce;
        public float CurrentLaunchForce { get => currentLaunchForce; set => currentLaunchForce = value; }

        public float ChargeSpeed { get; }
        public bool IsFired { get; set; }


        private PlayerService playerInstance = PlayerService.Instance;

        public TankController(TankModel tankModel, TankView tankPrefab, Vector3 spawnPosition)
        {
            TankModel = tankModel;
            TankView = Object.Instantiate(tankPrefab, spawnPosition, Quaternion.identity);
            TankView.SetColorOnAllRenderers(TankModel.Color);
            ChargeSpeed = (TankModel.MaxLaunchForce - TankModel.MinLaunchForce) / TankModel.MaxChargeTime;
            //IHealth health = TankView.gameObject.GetComponent<IHealth>();
            //health.SetHealth(tankModel.Health);
        }

        // health related logic
        public void KillTank()
        {
            ChangeHealth(TankModel.Health);
            OnDeath();
        }

        public void TakeDamage(Vector3 impactPosition, float _explosionRadius, float _MaxDamage)
        {
            float amount = CalculateDamage(impactPosition, _explosionRadius, _MaxDamage);
            ChangeHealth(amount);
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
        private void ChangeHealth(float amountValue)
        {
            TankModel.Health -= amountValue;
            //EventService.Instance.InvokeHealthChangeEvent(TankModel.Health, TankModel.TankIndex);
            EventService.Instance.InvokeHealthChangeEvent();
        }

        private void OnDeath()
        {
            TankModel.IsDead = true;
            TankView.InstantiateOnDeath();

            if(TankModel.TankTypes == TankType.Player)
            {
                playerInstance.InvokePlayerDeathEvent();
            }
            else if(TankModel.TankTypes == TankType.Enemy && !playerInstance.GetTankController().TankModel.IsDead)
            {
                EnemyService.Instance.ReduceEnemyList(this);
                playerInstance.IncrementEnemyKilledScore();
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
                    playerInstance.IncrementBulletsFiredScore();
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