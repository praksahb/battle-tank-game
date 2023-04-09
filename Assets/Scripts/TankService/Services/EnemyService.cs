using System.Collections.Generic;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyService : GenericMonoSingleton<EnemyService>
    {
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private int numOfEnemies;

        private int enemyTankIndex = 1;
        private TankController enemyTankController;

        private Stack<TankController> enemiesList;

        protected override void Awake()
        {
            base.Awake();
            enemiesList = new Stack<TankController>(numOfEnemies);
        }

        public void CreateEnemies()
        {
            if (enemiesList.Count == 0 && numOfEnemies == spawnPoint.Length)
            {
                for (int i = 0; i < numOfEnemies; i++)
                {
                    enemyTankController = CreateTank.CreateTankService.Instance.CreateTank(spawnPoint[i].position, enemyTankIndex);
                    enemiesList.Push(enemyTankController);
                    CameraController.Instance.AddTransformToTarget(enemyTankController.TankView.transform);
                    enemyTankController = null;
                }
            }
        }

        public int GetNumberOfEnemies()
        {
            return enemiesList.Count;
        }

        // Should work with List as well. If using The Equals to check just use List.Remove
        public void ReduceEnemyList(TankController _enemyTankController)
        {
            Stack<TankController> temp = new Stack<TankController>();
            while (enemiesList.Count > 0)
            {
                TankController tankController = enemiesList.Peek();
                if (tankController.Equals(_enemyTankController))
                {
                    // delete the value tankController from DS
                }
                else
                {
                    temp.Push(tankController);
                }
                enemiesList.Pop();
            }
            while (temp.Count > 0)
            {
                enemiesList.Push(temp.Pop());
            }

            if(enemiesList.Count == 0)
            {
                GameManager.Instance.LoadGameWon();
            }
        }

        public TankController GetAEnemyTankController()
        {
            return enemiesList.Pop();
        }
    }
}
