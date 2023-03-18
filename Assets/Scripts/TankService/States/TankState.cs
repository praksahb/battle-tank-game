using UnityEngine;

namespace TankBattle.Tank.EnemyTank
{
    [RequireComponent(typeof(EnemyTankView))]
    public class TankState : MonoBehaviour
    {
        //protected TankView tankView;
        protected EnemyTankView enemyTankView;
        [SerializeField] protected Color32 color;

        private void Awake()
        {
            //tankView = GetComponent<TankView>();
            enemyTankView = GetComponent<EnemyTankView>();
        }

        public virtual void OnEnterState() 
        {
            this.enabled = true;
        }
        public virtual void OnExitState() 
        {
            this.enabled = false;
        }

        // can use update as inheriting from monobehaviour
        //public virtual void Tick() { }
    }
}
