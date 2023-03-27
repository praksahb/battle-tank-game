using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class BulletObjectPool : MonoBehaviour
    {
        public static BulletObjectPool SharedInstance;

        [SerializeField] private BulletObjectPool bulletPool;
        [SerializeField] private ParticleSystem explosionParticles;

        void Awake()
        {
            SharedInstance = this;
        }

        private List<ShellController> pooledBullets;
        private Stack<ParticleSystem> poolParticleSystem;
        public int amountToPool;

        private void Start()
        {
            LoadBulletsPool();
            LoadParticlesPool();
        }

        private void LoadBulletsPool()
        {
            pooledBullets = new List<ShellController>(amountToPool);
            for (int i = 0; i < amountToPool; i++)
            {
                ShellController bullet = CreateShellService.Instance.CreateShell(gameObject.transform);
                bullet.GetShellView.SetInactive();
                pooledBullets.Add(bullet);
            }
        }

        private void LoadParticlesPool()
        {
            poolParticleSystem = new Stack<ParticleSystem>();
            for(int i = 0; i < amountToPool; i++)
            {
                ParticleSystem explosionParticle = Instantiate(explosionParticles, transform);
                poolParticleSystem.Push(explosionParticle);
            }
        }

        public ParticleSystem GetExplosionParticle()
        {
            while(poolParticleSystem.Peek() != null)
            {
                return poolParticleSystem.Pop();
            }
            return null;
        }

        async public void PushToParticlePool(ParticleSystem explosionParticle)
        {
            await Task.Delay((int)(explosionParticle.main.duration * 1000));
            poolParticleSystem.Push(explosionParticle);
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
            return null;
        }
    }
}
