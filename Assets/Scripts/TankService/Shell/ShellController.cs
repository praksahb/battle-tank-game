using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellController
    {
        public ShellModel GetShellModel { get; }
        public ShellView GetShellView { get; }

        public ShellController(Transform parentObj, ShellModel getShellModel, ShellView shellViewPrefab)
        {
            GetShellModel = getShellModel;
            GetShellView = Object.Instantiate(shellViewPrefab, parentObj);
        }

        public void CheckHitColliders(Collider[] hitColliders, int numOfColliders, Vector3 bulletPosition)
        {
            for (int i = 0; i < numOfColliders; i++)
            {
                Rigidbody targetRb = hitColliders[i].attachedRigidbody;

                // go to next collider
                if (!targetRb) continue;

                targetRb.AddExplosionForce(CreateShellService.Instance.GetBulletModel.ExplosionForce, bulletPosition, CreateShellService.Instance.GetBulletModel.ExplosionRadius);

                TankView targetTankView = targetRb.GetComponent<TankView>();
                TankController targetTank = targetTankView.GetTankController();
                if (targetTank == null) continue;

                float damage = CalculateDamage(targetTankView.transform.position, bulletPosition);
                targetTank.TakeDamage(damage);
            }
        }

        private float CalculateDamage(Vector3 tankPosition, Vector3 impactPosition)
        {
            float explosionDistance = (tankPosition - impactPosition).sqrMagnitude;

            float relativeDistance = (GetShellModel.ExplosionRadius - explosionDistance) / GetShellModel.ExplosionRadius;

            float damage = relativeDistance * GetShellModel.MaxDamage;
            damage = Mathf.Max(damage, 0f);
            return damage;
        }
    }
}
