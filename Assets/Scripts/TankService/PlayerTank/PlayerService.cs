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

        public event Action OnPlayerDeath;


        private void Start()
        {
            CreateTank();
            CameraController.Instance.AddTransformToTarget(tankController.TankView.transform);

        }

        public void CreateTank()
        {
            tankController = Tank.CreateTank.CreateTankService.Instance.CreateTank(spawnPoint.position, playerTankIndex);
        }

        public void InvokePlayerDeathEvent()
        {
            OnPlayerDeath?.Invoke();
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
            return tankController.TankView.transform;
        }
    };
}