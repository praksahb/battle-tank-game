using TankBattle.Tank;
using UnityEngine;

namespace TankBattle.Services
{
    // class name suggestions collectibleController
    // or collectibleUI or collectibleService
    public class Collectibles : MonoBehaviour
    {
        private TankController playerTankController;
        private void OnTriggerEnter(Collider other)
        {
            
            TankView tankView = other.gameObject.GetComponent<TankView>();
            if (tankView != null)
            {
                playerTankController = tankView.GetTankController();

                if(playerTankController.TankModel.TankTypes == TankType.Player)
                {
                    playerTankController.TankModel.BallsCollected++;
                    EventService.Instance.InvokeOnBallCollected(playerTankController.TankModel.BallsCollected);
                    Destroy(gameObject);
                }
            }
        }
    }
}
