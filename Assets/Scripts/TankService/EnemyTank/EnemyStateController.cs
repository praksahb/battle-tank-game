﻿

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyStateController
    {
        private TankController tankController;
        private EnemyTankView enemyTankView;


        //constructor
        public EnemyStateController(TankController _tankController, EnemyTankView _enemyTankView)
        {
            tankController = _tankController;
            enemyTankView = _enemyTankView;
        }

        public void PerformFireFunction()
        {
            tankController.CurrentLaunchForce = tankController.TankModel.MinLaunchForce;
            tankController.Fire();
        }
    }
}
