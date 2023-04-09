using System.Collections.Generic;
using System.Threading.Tasks;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class BulletServicePool : GenericMonoPool<BulletController>
    {
        public int amountToPool;
        [SerializeField] private ParticleSystem explosionParticles;
        private BulletController bulletController;

        private GenericPooling<ParticleSystem> poolParticleSystem;

        protected override BulletController CreateItem()
        {
            return BulletService.Instance.CreateBullet();
        }
        private void Start()
        {
            poolParticleSystem = new GenericPooling<ParticleSystem>(amountToPool, explosionParticles, transform);
        }

        public BulletController GetBullet()
        {

            bulletController = GetItem();
            bulletController.BulletView.Enable();
            bulletController.BulletView.transform.parent = transform;
            return bulletController;
        }

        public ParticleSystem GetExplosionParticle()
        {
            return poolParticleSystem.GetItem();
        }

        public void PushBulletBack(BulletController bulletController)
        {
            bulletController.BulletView.Disable();
            bulletController.BulletModel.SentBy = TankType.None;
            ReturnItem(bulletController);
        }

          async public void PushToParticlePool(ParticleSystem explosionParticle)
        {
            explosionParticle.transform.parent = transform;
            await Task.Delay((int)(explosionParticle.main.duration * 1000)); // time unit conversion
            poolParticleSystem.FreeItem(explosionParticle);

        }
    }
}
