using System.Collections.Generic;
using System.Threading.Tasks;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellServicePool : GenericMonoPool<ShellController>
    {
        public int amountToPool;
        [SerializeField] private ParticleSystem explosionParticles;
        private ShellController shellController;

        private GenericPooling<ParticleSystem> poolParticleSystem;

        protected override ShellController CreateItem()
        {
            return ShellService.Instance.CreateShell();
        }
        private void Start()
        {
            //LoadBulletsPool();

            poolParticleSystem = new GenericPooling<ParticleSystem>(amountToPool, explosionParticles, transform);
        }

        public ShellController GetBullet()
        {

            shellController = GetItem();
            shellController.ShellView.Enable();
            shellController.ShellView.transform.parent = transform;
            return shellController;
        }

        public ParticleSystem GetExplosionParticle()
        {
            return poolParticleSystem.GetItem();
        }

        public void PushBulletBack(ShellController shellController)
        {
            shellController.ShellView.Disable();
            shellController.ShellModel.SentBy = TankType.None;
            ReturnItem(shellController);
        }

          async public void PushToParticlePool(ParticleSystem explosionParticle)
        {
            explosionParticle.transform.parent = transform;
            await Task.Delay((int)(explosionParticle.main.duration * 1000));
            poolParticleSystem.FreeItem(explosionParticle);

        }
    }
}
