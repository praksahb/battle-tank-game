using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankIdleState : TankState
    {

        public override void OnEnterState()
        {
            base.OnEnterState();
            if(enemyAgent && !enemyAgent.isStopped)
            {
                enemyAgent.isStopped = true;
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("Entering State: " + enemyTankView.GetCurrentState());
            enemyAgent.isStopped = false;
        }

        private void Start()
        {
        }

    }
}
