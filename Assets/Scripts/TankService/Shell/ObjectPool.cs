using System.Collections.Generic;
using System.Threading.Tasks;
using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;

        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private ParticleSystem explosionParticles;
        public int amountToPool;

        private List<ShellController> pooledBullets;
        private Stack<ParticleSystem> poolParticleSystem;

        //private GenericPooling<ParticleSystem> poolParticleSystem;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            LoadBulletsPool();
            LoadParticlesPool();

            //poolParticleSystem = new GenericPooling<ParticleSystem>(amountToPool, explosionParticles, transform);
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

        private void LoadParticlesPool()
        {
            poolParticleSystem = new Stack<ParticleSystem>();
            for (int i = 0; i < amountToPool; i++)
            {
                ParticleSystem explosionParticle = Instantiate(explosionParticles, transform);
                poolParticleSystem.Push(explosionParticle);
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
            if (poolParticleSystem.Count > 0)
            {
                return poolParticleSystem.Pop();
            }
            ParticleSystem explosionParticle = Instantiate(explosionParticles, transform);
            return explosionParticle;


            //return poolParticleSystem.GetItem();
        }

        public void PushBulletBack(ShellController shellController)
        {
            shellController.GetShellView.SetInactive();
            shellController.GetShellModel.SentBy = TankType.None;
        }

          async public void PushToParticlePool(ParticleSystem explosionParticle)
        {
           // goes back to bulletPool empty game object
            explosionParticle.transform.parent = transform;
            // wait for explosion particle to stop playing
            // then push back to stack
            await Task.Delay((int)(explosionParticle.main.duration * 1000));
            poolParticleSystem.Push(explosionParticle);

            //poolParticleSystem.FreeItem(explosionParticle);
        }
    }
}
