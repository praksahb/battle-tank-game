using TankBattle.Services;
using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.UI;
using UnityEngine;

namespace TankBattle.Tank.CreateTank
{
    public class CreateTankService : GenericMonoSingleton<CreateTankService>
    {
        [SerializeField] private TankTypes.TankScriptableObjectList tankList;
        private TankModel tankModel;

        //temp value to store unique index for each tank - for differentiating in healthUI
        private int randomIdStart = 1000;
        public TankController CreateTank(Vector3 spawnPoint, int tankTypeIndex)
        {
            TankTypes.TankScriptableObject tankScriptableObject = tankList.tanks[tankTypeIndex];
            tankModel = new TankModel(tankScriptableObject);
            TankController tankController = new TankController(tankModel, tankScriptableObject.tankView, spawnPoint);
            tankController.TankView.SetTankController(tankController);
            tankController.TankModel.SetTankIndex(randomIdStart++);
            return tankController;

        }
    }
}