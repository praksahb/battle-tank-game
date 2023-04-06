using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankAttackingState : TankState
    {
        [SerializeField] private float fireCooldownTimer = 0.25f;
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

            if (enemyTankView.LookForPlayer(playerTransform))
            {
                if (timeElapsed >= fireCooldownTimer)
                {
                    timeElapsed = 0f;
                    if (playerTransform)
                        enemyAgent.transform.LookAt(playerTransform.position);
                    enemyStateController.PerformFireFunction();
                }
            }
            else if(playerTransform != null)
            {
                enemyTankView.ChangeState(enemyTankView.patrollingState);
            } else
            {
                enemyTankView.ChangeState(enemyTankView.idleState);
            }

        }
    }
}
