using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankChasingState : TankState
    {
        [SerializeField]
        private Color differentColor;

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering State: " + enemyTankView.GetCurrentState());
            enemyTankView.ChangeColor(differentColor);

        }

        public override void OnExitState()
        {
            base.OnExitState();
        }

        private void Update()
        {
            if (playerTransform != null && enemyTankView.LookForPlayer(playerTransform) != false)
            {
                if (enemyAgent.remainingDistance > enemyAgent.stoppingDistance)
                {
                    enemyAgent.SetDestination(playerTransform.position);
                }
                else
                {
                    enemyTankView.ChangeState(enemyTankView.attackingState);
                }
            }
        }
    }
}
