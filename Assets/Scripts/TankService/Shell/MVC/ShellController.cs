using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellController
    {
        public ShellModel ShellModel { get; }
        public ShellView ShellView { get; }

        public ShellController(ShellModel getShellModel, ShellView shellViewPrefab)
        {
            ShellModel = getShellModel;
            ShellView = Object.Instantiate(shellViewPrefab);
        }

        public void CheckHitColliders(Collider[] hitColliders, int numOfColliders, Vector3 bulletPosition)
        {
            for (int i = 0; i < numOfColliders; i++)
            {
                Rigidbody targetRb = hitColliders[i].attachedRigidbody;

                // hit if rigidbody is present
                if (targetRb)
                {
                    targetRb.AddExplosionForce(ShellService.Instance.BulletModel.ExplosionForce, bulletPosition, ShellService.Instance.BulletModel.ExplosionRadius);

                    TankView targetTankView = targetRb.GetComponent<TankView>();
                    if(targetTankView)
                    {
                        TankController targetTank = targetTankView.GetTankController();
                        if (targetTank != null)
                        {
                            float damage = CalculateDamage(targetTankView.transform.position, bulletPosition);
                            targetTank.TakeDamage(damage);
                        }
                    }
                }
            }
        }

        private float CalculateDamage(Vector3 tankPosition, Vector3 impactPosition)
        {
            float explosionDistance = (tankPosition - impactPosition).sqrMagnitude;

            float relativeDistance = (ShellModel.ExplosionRadius - explosionDistance) / ShellModel.ExplosionRadius;

            float damage = relativeDistance * ShellModel.MaxDamage;
            damage = Mathf.Max(damage, 0f);
            return damage;
        }
    }
}
