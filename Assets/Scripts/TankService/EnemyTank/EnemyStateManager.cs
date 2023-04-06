using TankBattle.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyStateManager : MonoBehaviour
    {
        // TANK STATE related
        [SerializeField] private Image StateImage;

        private TankState currentState;
        [SerializeField] private TankState startingState;

        public TankPatrollingState patrollingState;
        public TankChasingState chasingState;
        public TankAttackingState attackingState;
        public TankIdleState idleState;

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;
            
        public LayerMask obstacleMask;

        public EnemyStateController enemyStateController;

        private void Start()
        {
            ChangeState(startingState);
        }

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

        public bool LookForPlayer(Transform playerTransform)
        {
            if (playerTransform == null)
            {
                return false;
            }
            Vector3 distToTarget = playerTransform.position - transform.position;
            distToTarget.y = 0;

            float distMagnitude = distToTarget.magnitude;
            // within viewing length/range
            if (distMagnitude <= viewRadius)
            {
                Vector3 distNormalized = distToTarget.normalized;
                // within forward viewing angle
                if (transform.IsInViewingAngle(distNormalized, viewAngle))
                {
                    //check if any obstruction
                    if (!Physics.Raycast(transform.position, distNormalized, distMagnitude, obstacleMask))
                    {
                        // player detected
                        return true;
                    }
                }
            }
            return false;
        }
    }

    //[CustomEditor(typeof(EnemyStateManager))]
    //public class FieldOfViewEditor : Editor
    //{
    //    private void OnSceneGUI()
    //    {
    //        EnemyStateManager enemyFOV = (EnemyStateManager)target;
    //        Handles.color = Color.white;
    //        Vector3 viewAngleA = enemyFOV.DirFromAngle(-enemyFOV.viewAngle / 2, false);
    //        Vector3 viewAngleB = enemyFOV.DirFromAngle(enemyFOV.viewAngle / 2, false);
    //        Handles.DrawWireArc(enemyFOV.transform.position, Vector3.up, viewAngleA, enemyFOV.viewAngle, enemyFOV.viewRadius);
    //        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngleA * enemyFOV.viewRadius);
    //        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngleB * enemyFOV.viewRadius);
    //    }
    //}
}
