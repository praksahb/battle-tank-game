using System;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.PlayerTank
{
    // Player Service : function 1 : call create player tank
    // function 2 -  give controller ref to playerMove
    public class PlayerService: GenericMonoSingleton<PlayerService>
    {
        [SerializeField] private Transform spawnPoint;

        private int playerTankIndex = 0;
        private TankController tankController;
        private bool isAlive;

        public event Action OnPlayerDeath;


        private void Start()
        {
            CreateTank();
            CameraController.Instance.AddTransformToTarget(tankController.TankView.transform);
        }

        public void CreateTank()
        {
            tankController = Tank.CreateTank.CreateTankService.Instance.CreateTank(spawnPoint.position, playerTankIndex);
            isAlive = true;
        }

        public void InvokePlayerDeathEvent()
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
        }
        public void IncrementBulletsFiredScore()
        {
            tankController.TankModel.BulletsFired++;
            EventService.Instance.InvokeAchievementCallEvent(tankController.TankModel.BulletsFired, AchievementType.BulletFired);
        }

        public void IncrementEnemyKilledScore()
        {
            tankController.TankModel.EnemiesKilled++;
            EventService.Instance.InvokeAchievementCallEvent(tankController.TankModel.EnemiesKilled, AchievementType.EnemiesKilled);
        }

        public void IncrementBallsCollectedScore()
        {
            tankController.TankModel.BallsCollected++;
            EventService.Instance.InvokeAchievementCallEvent(tankController.TankModel.BallsCollected, AchievementType.BallsCollected);
        }

        public TankController GetTankController()
        {
            return tankController;
        }

        public Transform GetPlayerTransform()
        {
            if (tankController == null && tankController.TankView == null)
            {
                return null;
            }
            return tankController.TankView.transform;
        }

        public bool IsAlive()
        {
            return isAlive;
        }
    };
}