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
                    targetRb.AddExplosionForce(ShellModel.ExplosionForce, bulletPosition, ShellModel.ExplosionRadius);
                    IDamageable damageable = targetRb.gameObject.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(bulletPosition, ShellModel.ExplosionRadius, ShellModel.MaxDamage);
                    }
                }
            }
        }
    }
}
