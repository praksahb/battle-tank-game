using System;
using System.Collections.Generic;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyService : GenericMonoSingleton<EnemyService>
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int numOfEnemies;

        private int enemyTankIndex = 1;
        private TankController enemyTankController;
        private EnemyStateController enemyStateController;

        private Stack<TankController> enemiesList;

        protected override void Awake()
        {
            base.Awake();
            enemiesList = new Stack<TankController>(numOfEnemies);
        }

        void Start()
        {
            for(int i = 0; i < numOfEnemies; i++)
            {
                enemyTankController = CreateTank.CreateTankService.Instance.CreateTank(spawnPoint.position, enemyTankIndex);
                enemiesList.Push(enemyTankController);
                CameraController.Instance.AddTransformToTarget(enemyTankController.TankView.transform);
                enemyTankController = null;
            }
        }

        public int GetNumberOfEnemies()
        {
            return enemiesList.Count;
        }

        // Should work with List as well. If using The Equals to check just use List.Remove
        public void ReduceEnemyList(TankController _enemyTankController)
        {
            Stack<TankController>  temp = new Stack<TankController>();
            while(enemiesList.Count > 0)
            {
                TankController tc = enemiesList.Peek();
                if(tc.Equals(_enemyTankController))
                {
                    // delete the value tc from DS
                } 
                else
                {
                    temp.Push(tc);
                }
                enemiesList.Pop();
            }
            while(temp.Count > 0)
            {
                enemiesList.Push(temp.Pop());
            }
        }

        public TankController GetAEnemyTankController()
        {
            return enemiesList.Pop();
        }
    }
}
