using TankBattle.Tank.PlayerTank;
using UnityEngine;
using UnityEngine.AI;

namespace TankBattle.Tank.EnemyTank
{
    [RequireComponent(typeof(EnemyStateManager))]
    public class TankState : MonoBehaviour
    {
        private TankView tankView;

        protected EnemyStateManager enemyStateManager;
        protected EnemyStateController enemyStateController;
        protected NavMeshAgent enemyAgent;
        protected Transform playerTransform; 

        [SerializeField] protected Color color;

        private void Awake()
        {
            tankView = GetComponent<TankView>();
            enemyStateManager = GetComponent<EnemyStateManager>();
            enemyAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            if(PlayerService.Instance.IsAlive())
            {
                playerTransform = PlayerService.Instance.GetPlayerTransform();
            }
            TankController tankController = tankView.GetTankController();
            if(tankController != null)
                enemyStateController = new EnemyStateController(tankController, enemyStateManager);
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
