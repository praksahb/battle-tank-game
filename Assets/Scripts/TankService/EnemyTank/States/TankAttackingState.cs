using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankAttackingState : TankState
    {
        // GREEN

        [SerializeField] private float fireCooldownTimer = 0.25f;
        private float timeElapsed;
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering State: " + enemyStateManager.GetCurrentState());
            enemyStateManager.ChangeColor(color);
            timeElapsed = fireCooldownTimer + 0.1f;
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;

            if(playerTransform == null)
            {
                enemyStateManager.ChangeState(enemyStateManager.idleState);
                return;
            }
            
            if (timeElapsed > fireCooldownTimer)
            {
                timeElapsed = 0f;
                if (playerTransform != null)
                    enemyAgent.transform.LookAt(playerTransform.position);
                enemyStateController.PerformFireFunction();
            }

            if(enemyStateManager.LookForPlayer(playerTransform) == false)
            {
                enemyStateManager.ChangeState(enemyStateManager.patrollingState);
            }
        }
    }
}
