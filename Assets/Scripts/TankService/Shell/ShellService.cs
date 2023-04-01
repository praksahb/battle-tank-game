
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellService : GenericMonoSingleton<ShellService>
    {
        [SerializeField] private ShellScriptableObject shellScriptableObject;
        
        private ParticleSystem explosionParticles;
        public ShellModel BulletModel { get; private set; }
        private ShellServicePool objectPooler;

        private void Start()
        {
            objectPooler = (ShellServicePool)ShellServicePool.Instance;
        }

        // instantiate bullet 
        public ShellController CreateShell()
        {
            BulletModel = new ShellModel(shellScriptableObject);
            ShellController bulletShell = new ShellController(BulletModel, shellScriptableObject.shellView);
            bulletShell.ShellView.SetShellController(bulletShell);
            bulletShell.ShellView.SetMaxTankColliders(BulletModel.MaxTanksBulletCanDamage);
            return bulletShell; 
        }

        public void LaunchBullet(Transform fireTransform, Vector3 velocityVector, TankType tankType)
        {
            ShellController bullet = objectPooler.GetBullet();
            if(bullet != null)
            {
                explosionParticles = objectPooler.GetExplosionParticle();
                if(explosionParticles != null)
                {
                    BulletEdit(fireTransform, velocityVector, tankType, bullet);
                }
                else
                {
                    Debug.Log("explosion Particle is null");
                }
            }
            else
            {
                Debug.Log($"Bullet: {bullet}");
            }
        }

        private void BulletEdit(Transform fireTransform, Vector3 velocityVector, TankType tankType, ShellController bullet)
        {
            bullet.ShellModel.SentBy = tankType;
            bullet.ShellView.SetBulletFromValue(bullet.ShellModel.SentBy);
            bullet.ShellView.SetExplosionParticle(explosionParticles);
            explosionParticles.transform.parent = bullet.ShellView.transform;
            bullet.ShellView.transform.SetPositionAndRotation(fireTransform.position, fireTransform.rotation);
            bullet.ShellView.Enable();
            bullet.ShellView.AddVelocity(velocityVector);
        }
    }
}