using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankChasingState : TankState
    {
        //  RED

        [SerializeField]
        private Color differentColor;

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering State: " + enemyStateManager.GetCurrentState());
            enemyStateManager.ChangeColor(differentColor);
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }

        private void Update()
        {
            if(playerTransform == null)
            {
                enemyStateManager.ChangeState(enemyStateManager.idleState);
                return;
            }

            if (enemyStateManager.LookForPlayer(playerTransform))
            {
                if (enemyAgent.remainingDistance > enemyAgent.stoppingDistance)
                {
                    enemyAgent.SetDestination(playerTransform.position);
                }
                else
                {
                    enemyStateManager.ChangeState(enemyStateManager.attackingState);
                }
            }
        }
    }
}
