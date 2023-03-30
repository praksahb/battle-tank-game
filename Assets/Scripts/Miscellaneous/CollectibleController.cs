using TankBattle.Tank;
using TankBattle.Tank.Bullets;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle.Services
{
    // class name suggestions collectibleController
    // or collectibleUI or collectibleService
    public class CollectibleController : MonoBehaviour
    {
        private TankController playerTankController;
        private void OnTriggerEnter(Collider other)
        {
            // If a bullet is landing on the collectible it is giving Null exception error
            // if trying other.gameObject.GetComponent<TankView>().GetTankController();

            TankView tankView = other.gameObject.GetComponent<TankView>();
            if(tankView)
            {
                playerTankController = tankView.GetTankController();
                if(playerTankController != null && playerTankController.TankModel.TankTypes == TankType.Player)
                {
                    PlayerService.Instance.IncrementBallsCollectedScore();
                    Destroy(gameObject);
                }
            } 
            else
            {
                // Bullet can destroy the collectible ball
                ShellView shellView = other.gameObject.GetComponent<ShellView>();
                if (shellView != null)
                {
                    if(shellView.GetBulletFrom() == TankType.Player)
                    {
                        PlayerService.Instance.IncrementBallsCollectedScore();
                        Destroy(gameObject);
                    }
                }
                
            }

            
            
        }
    }
}
