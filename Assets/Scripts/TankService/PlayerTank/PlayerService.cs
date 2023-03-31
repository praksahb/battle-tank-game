using System;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.PlayerTank
{
    // Player Service : function 1 : call create player tank
    // function 2 -  give controller ref to playerMove
    public class PlayerService: GenericSingleton<PlayerService>
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
            EventService.Instance.InvokeOnBulletFiredEvent(tankController.TankModel.BulletsFired);
        }

        public void IncrementEnemyKilledScore()
        {
            tankController.TankModel.EnemiesKilled++;
            EventService.Instance.InvokeOnEnemyKilled(tankController.TankModel.EnemiesKilled);
        }

        public void IncrementBallsCollectedScore()
        {
            tankController.TankModel.BallsCollected++;
            EventService.Instance.InvokeOnBallCollected(tankController.TankModel.BallsCollected);
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