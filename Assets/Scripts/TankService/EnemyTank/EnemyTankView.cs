using TankBattle.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Tank.EnemyTank
{
    public class EnemyTankView : MonoBehaviour
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

        //private void FindVisibleTargets()
        //{
        //    Collider[] targetsinViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //    for (int i = 0; i < targetsinViewRadius.Length; i++)
        //    {
        //        // visibleTargets.Clear();
        //        Transform target = targetsinViewRadius[i].transform;
        //        Vector3 dirToTarget = (target.position - transform.position).normalized;
        //        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        //        {
        //            float distToTarget = Vector3.Distance(transform.position, target.position);
        //            if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
        //            {
        //                // target is in view range of enemy -- can use list to store if more than one targets
        //                //visibleTargets.Add(target);
        //            }
        //        }
        //    }
        //}

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
                if (Vector3.Dot(distNormalized, transform.forward) > Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
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

    [CustomEditor(typeof(EnemyTankView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            EnemyTankView enemyFOV = (EnemyTankView)target;
            Handles.color = Color.white;
            Vector3 viewAngleA = enemyFOV.DirFromAngle(-enemyFOV.viewAngle / 2, false);
            Vector3 viewAngleB = enemyFOV.DirFromAngle(enemyFOV.viewAngle / 2, false);
            Handles.DrawWireArc(enemyFOV.transform.position, Vector3.up, viewAngleA, enemyFOV.viewAngle, enemyFOV.viewRadius);
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngleA * enemyFOV.viewRadius);
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngleB * enemyFOV.viewRadius);
        }
    }
}
