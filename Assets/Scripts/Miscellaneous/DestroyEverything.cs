using System.Collections;
using TankBattle.Tank;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle
{
    public class DestroyEverything : MonoBehaviour
    {
        [SerializeField] private GameObject destroyObjectFloor;
        [SerializeField] private GameObject destroyObjectRest;
        [SerializeField] private float waitTimeSmall = 0.5f;
        [SerializeField] private float waitTimeLarge = 2f;

        private TankController enemyTankController;

        private Coroutine coroutine;
        private WaitForSeconds _waitSmall;
        private WaitForSeconds _waitLarge;
        private int numOfEnemies;

        private void Awake()
        {
            _waitSmall = new WaitForSeconds(waitTimeSmall);
            _waitLarge = new WaitForSeconds(waitTimeLarge);

        }

        private void OnEnable()
        {
            PlayerService.Instance.OnPlayerDeath += RunCoroutine;

        }

        private void OnDisable()
        {
            PlayerService.Instance.OnPlayerDeath -= RunCoroutine;
        }

        private void OnDestroy()
        {
            _waitSmall = null;
            _waitLarge = null;
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
            numOfEnemies = EnemyService.Instance.GetNumberOfEnemies();
            yield return _waitLarge;
            for(int i = 0; i < numOfEnemies; i++)
            {
                DeathRoutineEnemy(i);
                yield return _waitSmall;
            }
            yield return _waitLarge;
            DestroyFloor();
            yield return _waitLarge;
            DestroyWorld();
        }

        private void DeathRoutineEnemy(int idx)
        {
            enemyTankController = EnemyService.Instance.GetEnemyTankControllerByIndex(idx);
            if(enemyTankController != null)
            {
                enemyTankController.TakeDamage(enemyTankController.TankModel.Health);
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
