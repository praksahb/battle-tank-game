using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyTankView : MonoBehaviour
    {
        // TANK STATE related
        [SerializeField]
        private Image StateImage;

        private TankState currentState;
        [SerializeField]
        private TankState startingState;

        [SerializeField]
        public TankPatrollingState patrollingState;
        [SerializeField]
        public TankChasingState chasingState;
        [SerializeField]
        public TankAttackingState attackingState;

        public EnemyStateController enemyStateController;

        public TankState GetCurrentState()
        {
            return currentState;
        }

        public void ChangeColor(Color32 color)
        {
            // alpha value is 0 if not changed
            color.a = 255;
            Image image = StateImage.GetComponent<Image>();
            image.color = color;
        }

        public void ChangeState(TankState newState)
        {
            if (currentState != null)
            {
                currentState.OnExitState();
            }
            currentState = newState;
            currentState.OnEnterState();
        }

        private void Start()
        {
            ChangeState(startingState);
        }
    }
}
