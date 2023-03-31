using UnityEngine;

namespace TankBattle.Services
{
    // generic object pool implementation
    public class ParticlePool : MonoBehaviour
    {
        public ParticleSystem _particleSystem;
        public int poolSize;

        private GenericPooling<ParticleSystem> particlePool;

        private void Start()
        {
            particlePool = new GenericPooling<ParticleSystem>(poolSize, _particleSystem, transform);
        }
    }
}
