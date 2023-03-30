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

        private int maxTankColliders;
        private TankType bulletFrom;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            explosionAudio = GetComponent<AudioSource>();
        }

        public TankType GetBulletFrom()
        {
            return bulletFrom;
        }
        public void SetBulletFromValue(TankType tankType)
        {
            bulletFrom = tankType;
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

        public void SetMaxTankColliders(int maxTanksBulletCanDamage)
        {
            maxTankColliders = maxTanksBulletCanDamage;
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
            // maxTankColliders get value from shellModel when instantiating bullet/shell
            Collider[] hitColliders = new Collider[maxTankColliders];
            int numOfColliders = Physics.OverlapSphereNonAlloc(transform.position, shellController.GetShellModel.ExplosionRadius, hitColliders, shellController.GetShellModel.LayerMask);

            shellController.CheckHitColliders(hitColliders, numOfColliders, transform.position);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            explosionParticles.transform.parent = null;
            explosionParticles.Play();
            explosionAudio.Play();
            ObjectPool.SharedInstance.PushBulletBack(shellController);
            ObjectPool.SharedInstance.PushToParticlePool(explosionParticles);
        }

    }
}