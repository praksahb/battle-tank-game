using TankBattle.Tank.PlayerTank;
using UnityEngine;
using UnityEngine.AI;

namespace TankBattle.Tank.EnemyTank
{
    [RequireComponent(typeof(EnemyTankView))]
    public class TankState : MonoBehaviour
    {
        private TankView tankView;

        protected EnemyTankView enemyTankView;
        protected EnemyStateController enemyStateController;
        protected NavMeshAgent enemyAgent;
        protected Transform playerTransform; 

        [SerializeField] protected Color color;

        private void Awake()
        {
            tankView = GetComponent<TankView>();
            enemyTankView = GetComponent<EnemyTankView>();
            enemyAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            playerTransform = PlayerService.Instance.GetPlayerTransform();
            enemyStateController = new EnemyStateController(tankView.GetTankController(), enemyTankView);
        }

        public virtual void OnEnterState()
        {
            this.enabled = true;
        }
        public virtual void OnExitState()
        {
            this.enabled = false;
        }
    }
}
