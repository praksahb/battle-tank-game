using TankBattle.Tank.PlayerTank;
using UnityEngine;
using UnityEngine.AI;

namespace TankBattle.Tank.EnemyTank
{
    public class TankChasingState : TankState
    {
        [SerializeField]
        private Color differentColor;

        private Transform playerTransform;

        private void OnEnable()
        {
            playerTransform = PlayerService.Instance.GetPlayerTransform();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering State: " + enemyTankView.GetCurrentState());
            enemyTankView.ChangeColor(differentColor);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            playerTransform = null;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                enemyTankView.ChangeState(enemyTankView.patrollingState);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                enemyTankView.ChangeState(enemyTankView.attackingState);
            }

            if(playerTransform != null && enemyAgent.remainingDistance > enemyAgent.stoppingDistance)
            {
                enemyAgent.SetDestination(playerTransform.position);
            }
        }
    }
}
