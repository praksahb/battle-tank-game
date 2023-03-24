using TankBattle.Tank.PlayerTank;
using UnityEngine;

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
            tankController.CurrentLaunchForce = tankController.GetTankModel.minLaunchForce;
            tankController.Fire();
        }
    }
}
