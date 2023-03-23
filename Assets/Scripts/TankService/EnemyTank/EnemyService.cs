using System.Collections.Generic;
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyService : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int numOfEnemies;

        private int enemyTankIndex = 1;
        private TankController enemyTankController;
        private EnemyStateController enemyStateController;

        private List<TankController> enemiesList;

        private void Awake()
        {
            enemiesList = new List<TankController>(numOfEnemies);
        }

        void Start()
        {
            for(int i = 0; i < numOfEnemies; i++)
            {
                enemyTankController = CreateTank.CreateTankService.Instance.CreateTank(spawnPoint.position, enemyTankIndex);
                enemiesList.Add(enemyTankController);
                enemyTankController = null;
            }
        }

        public int GetNumberOfEnemies()
        {
            return enemiesList.Count;
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
