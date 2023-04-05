using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TankBattle.Extensions;
using System.Collections.Generic;
using System.Collections;

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

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public List<Transform> visibleTargets = new List<Transform>();

        public EnemyStateController enemyStateController;

        private Coroutine searchPlayerCoroutine;

        private void Start()
        {
            ChangeState(startingState);

            if(searchPlayerCoroutine != null)
            {
                StopCoroutine(searchPlayerCoroutine);
            }
            searchPlayerCoroutine = StartCoroutine(FindTargetsWithDelay(0.2f));
        }

        private IEnumerator FindTargetsWithDelay(float delay)
        {
            while(true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
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

        private void FindVisibleTargets()
        {
            Collider[] targetsinViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for(int i =0; i < targetsinViewRadius.Length; i++)
            {
                visibleTargets.Clear();
                Transform target = targetsinViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);
                    if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        // target is in view range of enemy -- can use list to store if more than one targets
                        visibleTargets.Add(target);
                    }
                }
            }
        }
    }

    [CustomEditor (typeof (EnemyTankView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            EnemyTankView enemyFOV = (EnemyTankView)target;
            Handles.color = Color.white;
            Vector3 viewAngleA = enemyFOV.DirFromAngle(-enemyFOV.viewAngle / 2, false);
            Vector3 viewAngleB = enemyFOV.DirFromAngle(enemyFOV.viewAngle / 2, false);
            Handles.DrawWireArc(enemyFOV.transform.position, Vector3.up, viewAngleA, enemyFOV.viewAngle, enemyFOV.viewRadius);
            Handles.DrawLine (enemyFOV.transform.position,enemyFOV.transform.position + viewAngleA * enemyFOV.viewRadius);
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngleB * enemyFOV.viewRadius);

            Handles.color = Color.red;
            for(int i = 0; i < enemyFOV.visibleTargets.Count; i++)
            {
                Handles.DrawLine(enemyFOV.transform.position, enemyFOV.visibleTargets[i].transform.position);
            }
        }
    }
}
