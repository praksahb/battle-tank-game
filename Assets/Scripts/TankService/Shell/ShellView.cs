using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShellView : MonoBehaviour
    {
        private ParticleSystem explosionParticles;
        private Coroutine coroutine;

        private ShellController shellController;
        private AudioSource explosionAudio;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            explosionAudio = GetComponent<AudioSource>();
        }

        public void SetInactive()
        {
            gameObject.SetActive(false);
        }

        public void SetActive()
        {
            gameObject.SetActive(true);
        }

        public bool CheckIsActive()
        {
            return gameObject.activeInHierarchy;
        }

        public void SetShellController(ShellController _shellController)
        {
            shellController = _shellController;
        }

        public void SetExplosionParticle(ParticleSystem _explosionParticle)
        {
            explosionParticles = _explosionParticle;
            explosionParticles.transform.position = transform.position;
        }

        public void AddVelocity(Vector3 velocityVector)
        {
            rb.velocity = velocityVector;
        }

        // main function -
        // find only tank colliders in a sphere area around the shell and give damage
        // according to its distance away from it.
        private void OnTriggerEnter(Collider other)
        {
            int maxColliders = 10;
            Collider[] hitColliders = new Collider[maxColliders];
            int numOfColliders = Physics.OverlapSphereNonAlloc(transform.position, shellController.GetShellModel.ExplosionRadius, hitColliders, shellController.GetShellModel.LayerMask);

            shellController.CheckHitColliders(hitColliders, numOfColliders, transform.position);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            explosionParticles.transform.parent = null;
            explosionParticles.Play();
            explosionAudio.Play();
            SetInactive();
            BulletObjectPool.SharedInstance.PushToParticlePool(explosionParticles);
        }

    }
}