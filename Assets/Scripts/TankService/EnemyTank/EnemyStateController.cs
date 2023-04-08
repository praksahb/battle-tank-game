

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyStateController
    {
        private TankController tankController;
        private EnemyStateManager enemyStateManager;

        public EnemyStateController(TankController _tankController, EnemyStateManager _enemyTankView)
        {
            tankController = _tankController;
            enemyStateManager = _enemyTankView;
        }

        public void PerformFireFunction()
        {
            tankController.Fire();
        }
    }
}
