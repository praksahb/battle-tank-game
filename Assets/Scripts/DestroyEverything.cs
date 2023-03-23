using System.Collections;
using TankBattle.Tank;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle
{
    public class DestroyEverything : MonoBehaviour
    {
        [SerializeField] private EnemyService enemyService;
        [SerializeField] private GameObject destroyObjectFloor;
        [SerializeField] private GameObject destroyObjectRest;

        private TankController playerTankController;
        private TankController enemyTankController;

        private Coroutine coroutine;
        private WaitForSeconds _waitSmall;
        private WaitForSeconds _waitTwoSeconds;
        private int numOfEnemies;

        private void Awake()
        {
            // early caching or something
            _waitSmall = new WaitForSeconds(0.5f);
            _waitTwoSeconds = new WaitForSeconds(2f);
        }

        private IEnumerator Start()
        {
            yield return _waitSmall;
            playerTankController = PlayerService.Instance.GetTankController();
            playerTankController.OnPlayerDeath += RunCoroutine;
        }

        private void OnDisable()
        {
            playerTankController.OnPlayerDeath -= RunCoroutine;
        }

        public void RunCoroutine()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            numOfEnemies = enemyService.GetNumberOfEnemies();
            yield return _waitTwoSeconds;
            for(int i = 0; i < numOfEnemies; i++)
            {
                DeathRoutineEnemy(i);
                yield return _waitSmall;
            }
            yield return _waitTwoSeconds;
            DestroyFloor();
            yield return _waitTwoSeconds;
            DestroyWorld();
        }

        private void DeathRoutineEnemy(int idx)
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
