using System.Collections;
using TankBattle.Tank;
using TankBattle.Tank.EnemyTank;
using UnityEngine;

namespace TankBattle
{
    public class DestroyEverything : GenericSingleton<DestroyEverything>
    {
        [SerializeField] private EnemyService enemyService;
        [SerializeField] private GameObject destroyObjectFloor;
        [SerializeField] private GameObject destroyObjectRest;


        private TankController enemyTankController;

        private Coroutine coroutine;
        private WaitForSeconds _wait;
        private int numOfEnemies;

        public void RunCoroutine()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(deathRoutine());
        }


        private IEnumerator deathRoutine()
        {
            _wait = new WaitForSeconds(2f);
            numOfEnemies = enemyService.GetNumberOfEnemies();
            yield return _wait;
            for(int i = 0; i < numOfEnemies; i++)
            {
                deathRoutineEnemy(i);
            }
            yield return _wait;
            DestroyFloor();
            yield return _wait;
            DestroyWorld();
        }

        private void deathRoutineEnemy(int idx)
        {
            enemyTankController = enemyService.GetEnemyTankControllerByIndex(idx);
            if(enemyTankController != null)
            {
                enemyTankController.TakeDamage(enemyTankController.GetTankModel.GetSetHealth);
            }
        }

        private void DestroyFloor()
        {
            Destroy(destroyObjectFloor);
        }

        private void DestroyWorld()
        {
            Destroy(destroyObjectRest);
        }
    }
}
