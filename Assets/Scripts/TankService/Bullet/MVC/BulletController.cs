using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class BulletController
    {
        public BulletModel BulletModel { get; }
        public BulletView BulletView { get; }

        public BulletController(BulletModel getShellModel, BulletView shellViewPrefab)
        {
            BulletModel = getShellModel;
            BulletView = Object.Instantiate(shellViewPrefab);
        }

        public void CheckHitColliders(Collider[] hitColliders, int numOfColliders, Vector3 bulletPosition)
        {
            for (int i = 0; i < numOfColliders; i++)
            {
                Rigidbody targetRb = hitColliders[i].attachedRigidbody;

                // hit if rigidbody is present
                if (targetRb)
                {
                    targetRb.AddExplosionForce(BulletModel.ExplosionForce, bulletPosition, BulletModel.ExplosionRadius);
                    IDamageable damageable = targetRb.gameObject.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(bulletPosition, BulletModel.ExplosionRadius, BulletModel.MaxDamage);
                    }
                }
            }
        }
    }
}
