using System.Collections;
using TankBattle.Tank;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace TankBattle
{
    public class DestroyEverything : MonoBehaviour
    {
        [SerializeField] private GameObject destroyObjectFloor;
        [SerializeField] private GameObject destroyObjectRest;
        [SerializeField] private GameObject lightSource;
        [SerializeField] private float waitTimeSmall = 0.5f;
        [SerializeField] private float waitTimeLarge = 2f;

        private TankController enemyTankController;

        private Coroutine DestroyEverythingCoroutine;
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
            if(DestroyEverythingCoroutine != null)
            {
                StopCoroutine(DestroyEverythingCoroutine);
            }
            DestroyEverythingCoroutine = StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            numOfEnemies = EnemyService.Instance.GetNumberOfEnemies();

            UnparentGameObjects();
            yield return _waitLarge;
            for (int i = 0; i < numOfEnemies; i++)
            {
                DeathRoutineEnemy();
                yield return _waitSmall;
            }
            yield return _waitSmall;
            DestroyWorld();
            yield return _waitSmall;
            DestroyFloor();
        }

        private void UnparentGameObjects()
        {
            lightSource.transform.parent = null;
            destroyObjectFloor.transform.parent = null;
        }

        private void DeathRoutineEnemy()
        {
            enemyTankController = EnemyService.Instance.GetAEnemyTankController();
            if(enemyTankController != null)
            {
                enemyTankController.KillTank();
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
