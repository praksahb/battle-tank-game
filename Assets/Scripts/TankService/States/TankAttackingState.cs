
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankAttackingState : TankState
    {

        private float timeElapsed = 0f;

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering State: " + enemyTankView.GetCurrentState());
            enemyTankView.ChangeColor(color);
        }

        public override void OnExitState()
        {
            base.OnExitState();

        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                enemyTankView.ChangeState(enemyTankView.patrollingState);
            }

            if(timeElapsed >= 1f)
            {
                timeElapsed = 0f;
                if(playerTransform)
                    enemyAgent.transform.LookAt(playerTransform.position);
                enemyStateController.PerformFireFunction();
            }
        }
    }
}
