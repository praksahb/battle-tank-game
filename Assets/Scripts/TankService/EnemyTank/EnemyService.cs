using System;
using System.Collections.Generic;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyService : GenericSingleton<EnemyService>
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int numOfEnemies;

        private int enemyTankIndex = 1;
        private TankController enemyTankController;
        private EnemyStateController enemyStateController;

        private List<TankController> enemiesList;

        protected override void Awake()
        {
            base.Awake();
            enemiesList = new List<TankController>(numOfEnemies);
        }

        void Start()
        {
            for(int i = 0; i < numOfEnemies; i++)
            {
                enemyTankController = CreateTank.CreateTankService.Instance.CreateTank(spawnPoint.position, enemyTankIndex);
                enemiesList.Add(enemyTankController);
                CameraController.Instance.AddTransformToTarget(enemyTankController.TankView.transform);
                enemyTankController = null;
            }
        }

        public int GetNumberOfEnemies()
        {
            return enemiesList.Count;
        }


        // Bug - some enemies dont get killed in game after deathRoutine
        public void ReduceEnemyList(TankController _enemyTankController)
        {
            enemiesList.Remove(_enemyTankController);
        }

        public TankController GetEnemyTankControllerByIndex(int index)
        {
            if(index < 0 || index >= enemiesList.Count)
            {
                return null;
            }
            return enemiesList[index];
        }
    }
}
