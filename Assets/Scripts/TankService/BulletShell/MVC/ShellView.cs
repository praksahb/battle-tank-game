using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShellView : MonoBehaviour
    {
        private ParticleSystem explosionParticles;

        private ShellController shellController;
        private AudioSource explosionAudio;
        private Rigidbody rigidBody;

        private int maxTankColliders;
        private TankType bulletFrom;

        private ShellServicePool bulletPool;

        private void Start()
        {
            bulletPool = (ShellServicePool)ShellServicePool.Instance;
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
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

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public bool CheckIsEnabled()
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
            rigidBody.velocity = velocityVector;
        }

        // main function -
        // find only tank colliders in a sphere area around the shell and give damage
        // according to its distance away from it.
        private void OnTriggerEnter(Collider other)
        {
            // maxTankColliders get value from shellModel when instantiating bullet/shell
            Collider[] hitColliders = new Collider[maxTankColliders];
            int numOfColliders = Physics.OverlapSphereNonAlloc(transform.position, shellController.ShellModel.ExplosionRadius, hitColliders, shellController.ShellModel.LayerMask);

            shellController.CheckHitColliders(hitColliders, numOfColliders, transform.position);
            DestroyBullet();
            explosionParticles = null;
        }

        private void DestroyBullet()
        {
            explosionParticles.transform.parent = null;
            explosionParticles.Play();
            explosionAudio.Play();
            bulletPool.PushBulletBack(shellController);
            bulletPool.PushToParticlePool(explosionParticles);
        }

    }
}