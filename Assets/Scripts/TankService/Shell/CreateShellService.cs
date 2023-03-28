
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class CreateShellService : GenericSingleton<CreateShellService>
    {
        [SerializeField] private ShellScriptableObject shellScriptableObject;
        
        private ParticleSystem explosionParticles;

        public ShellModel GetBulletModel { get; set; }

        // instantiate bullet 
        public ShellController CreateShell(Transform parentTransform)
        {
            GetBulletModel = new ShellModel(shellScriptableObject); 
            ShellController bulletShell = new ShellController(parentTransform, GetBulletModel, shellScriptableObject.shellView);
            bulletShell.GetShellView.SetShellController(bulletShell);
            return bulletShell;
        }

        public void LaunchBullet(Transform fireTransform, Vector3 velocityVector)
        {
            ShellController bullet = BulletObjectPool.SharedInstance.GetBullet();
            if(bullet != null)
            {
                explosionParticles = BulletObjectPool.SharedInstance.GetExplosionParticle();

                if(explosionParticles != null)
                {
                    bullet.GetShellView.SetExplosionParticle(explosionParticles);
                    explosionParticles.transform.parent = bullet.GetShellView.transform;
                    bullet.GetShellView.transform.position = fireTransform.position;
                    bullet.GetShellView.transform.rotation = fireTransform.rotation;
                    bullet.GetShellView.SetActive();
                    bullet.GetShellView.AddVelocity(velocityVector);
                }
            }
        }

    }
}