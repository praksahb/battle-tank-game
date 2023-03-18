
using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    public class TankChasingState : TankState
    {
        [SerializeField]
        private Color32 differentColor;

        public override void OnEnterState()
        {
            base.OnEnterState();
            enemyTankView.ChangeColor(differentColor);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                enemyTankView.ChangeState(enemyTankView.patrollingState);
            }
        }
    }
}
