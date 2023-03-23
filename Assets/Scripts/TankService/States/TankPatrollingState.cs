using UnityEngine;
using UnityEngine.AI;

namespace TankBattle.Tank.EnemyTank
{
    public class TankPatrollingState : TankState
    {
        [SerializeField] private float radiusRange = 10f;


        public override void OnEnterState()
        {
            base.OnEnterState();
            if(enemyAgent)
            {
                enemyAgent.isStopped = false;
            }
            Debug.Log("Entering State: " + enemyTankView.GetCurrentState());
            enemyTankView.ChangeColor(color);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            // custom logic
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                enemyTankView.ChangeState(enemyTankView.chasingState);
            }

            // move Randomly
            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(enemyAgent.transform.position, radiusRange, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    enemyAgent.SetDestination(point);
                }
            }
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
    }
}
