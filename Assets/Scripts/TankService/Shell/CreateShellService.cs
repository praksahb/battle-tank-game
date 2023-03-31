
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class CreateShellService : GenericSingleton<CreateShellService>
    {
        [SerializeField] private ShellScriptableObject shellScriptableObject;
        
        private ParticleSystem explosionParticles;
        public ShellModel GetBulletModel { get; private set; }
        private ObjectPool objectPooler;

        private void Start()
        {
            objectPooler = ObjectPool.SharedInstance;
        }

        // instantiate bullet 
        public ShellController CreateShell(Transform parentTransform)
        {
            GetBulletModel = new ShellModel(shellScriptableObject);
            ShellController bulletShell = new ShellController(parentTransform, GetBulletModel, shellScriptableObject.shellView);
            bulletShell.GetShellView.SetShellController(bulletShell);
            bulletShell.GetShellView.SetMaxTankColliders(GetBulletModel.MaxTanksBulletCanDamage);
            return bulletShell; 
        }

        public void LaunchBullet(Transform fireTransform, Vector3 velocityVector, TankType tankType)
        {
            ShellController bullet = objectPooler.GetBullet();
            bullet.GetShellModel.SentBy = tankType;
            bullet.GetShellView.SetBulletFromValue(bullet.GetShellModel.SentBy);
            //bullet.GetShellView.SetActive();
            if(bullet != null)
            {
                explosionParticles = objectPooler.GetExplosionParticle();
                if(explosionParticles != null)
                {
                    bullet.GetShellView.SetExplosionParticle(explosionParticles);
                    explosionParticles.transform.parent = bullet.GetShellView.transform;
                    bullet.GetShellView.transform.position = fireTransform.position;
                    bullet.GetShellView.transform.rotation = fireTransform.rotation;
                    bullet.GetShellView.SetActive();
                    bullet.GetShellView.AddVelocity(velocityVector);
                }
                else
                {
                    Debug.Log("Chek1");
                }
            }
            else
            {
                Debug.Log($"Bullet: {bullet}");
            }
        }

    }
}