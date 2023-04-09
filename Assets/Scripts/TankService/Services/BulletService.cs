using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class BulletService : GenericMonoSingleton<BulletService>
    {
        [SerializeField] private BulletScriptableObject bulletScriptableObject;
        
        private ParticleSystem explosionParticles;
        public BulletModel BulletModel { get; private set; }
        private BulletServicePool objectPool;

        private void Start()
        {
            objectPool = (BulletServicePool)BulletServicePool.Instance;
        }

        // instantiate bullet 
        public BulletController CreateBullet()
        {
            BulletModel = new BulletModel(bulletScriptableObject);
            BulletController bulletShell = new BulletController(BulletModel, bulletScriptableObject.shellView);
            bulletShell.BulletView.SetBulletController(bulletShell);
            return bulletShell; 
        }

        public void LaunchBullet(Transform fireTransform, TankType tankType)
        {
            BulletController bullet = objectPool.GetBullet();
            if(bullet != null)
            {
                explosionParticles = objectPool.GetExplosionParticle();
                if(explosionParticles != null)
                {
                    bullet.BulletModel.SentBy = tankType;
                    bullet.BulletView.SetupBullet(fireTransform, explosionParticles);
                }
            }
        }
    }
}