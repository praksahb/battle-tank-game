using System.Collections.Generic;
using System.Threading.Tasks;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;

        [SerializeField] private ParticleSystem explosionParticles;

        public int amountToPool;

        private List<ShellController> pooledBullets;

        private GenericPooling<ParticleSystem> poolParticleSystem;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            LoadBulletsPool();

            poolParticleSystem = new GenericPooling<ParticleSystem>(amountToPool, explosionParticles, transform);
        }

        private void LoadBulletsPool()
        {
            pooledBullets = new List<ShellController>();
            for (int i = 0; i < amountToPool; i++)
            {
                ShellController bullet = CreateBullet();
                bullet.GetShellView.SetInactive();
                pooledBullets.Add(bullet);
            }
        }

        public ShellController GetBullet()
        {
            for (int i = 0; i < pooledBullets.Count; i++)
            {
                if (!pooledBullets[i].GetShellView.CheckIsActive())
                {
                    return pooledBullets[i];
                }
            }
            ShellController bullet = CreateBullet();
            bullet.GetShellView.SetInactive();
            return bullet;
        }

        private ShellController CreateBullet()
        {
            ShellController bullet = CreateShellService.Instance.CreateShell(gameObject.transform);
            return bullet;
        }

        public ParticleSystem GetExplosionParticle()
        {
            return poolParticleSystem.GetItem();
        }

        public void PushBulletBack(ShellController shellController)
        {
            shellController.GetShellView.SetInactive();
            shellController.GetShellModel.SentBy = TankType.None;
        }

          async public void PushToParticlePool(ParticleSystem explosionParticle)
        {
            explosionParticle.transform.parent = transform;
            await Task.Delay((int)(explosionParticle.main.duration * 1000));
            poolParticleSystem.FreeItem(explosionParticle);

        }
    }
}
