using TankBattle.Tank;
using TankBattle.Tank.PlayerTank;
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
            playerTankController = other.gameObject.GetComponent<TankView>().GetTankController();
                if(playerTankController != null && playerTankController.TankModel.TankTypes == TankType.Player)
                {
                    PlayerService.Instance.IncrementBallsCollectedScore();
                    Destroy(gameObject);
                }
            
        }
    }
}
