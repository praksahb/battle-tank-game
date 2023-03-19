using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
