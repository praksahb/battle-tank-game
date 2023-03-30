
using TankBattle.Services;
using UnityEngine;

namespace TankBattle
{
    // generic object pool implementation
    public class ParticlePool : MonoBehaviour
    {
        private GenericPooling<ParticleSystem> particlePool;

        private void Start()
        {
            particlePool = new GenericPooling<ParticleSystem>();
        }
    }
}
