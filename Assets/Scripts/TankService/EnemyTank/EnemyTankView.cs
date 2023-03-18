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

        public TankState GetCurrentState()
        {
            return currentState;
        }

        public void ChangeColor(Color32 color)
        {
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

        private void Awake()
        {
            //image.enabled = true;
        }

        private void Start()
        {
            ChangeState(startingState);
        }
    }
}
