using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : MonoBehaviour
    {


        private ParticleSystem explosionParticles;
        private AudioSource explosionAudio;

        private BulletController bulletController;
        private Rigidbody rigidBody;

        private int maxTankColliders;

        private BulletServicePool bulletPool;

        private void Start()
        {
            bulletPool = (BulletServicePool)BulletServicePool.Instance;
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            explosionAudio = GetComponent<AudioSource>();
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

        public void SetupBullet(Transform firePoint, ParticleSystem explosionParticle)
        {
            SetExplosionParticle(explosionParticle);
            transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            Vector3 bulletVelocity = bulletController.BulletModel.BulletLaunchForce * transform.forward;
            AddVelocity(bulletVelocity);
        }

        public void SetBulletController(BulletController _bulletController)
        {
            bulletController = _bulletController;
        }

        public BulletController GetBulletController()
        {
            return bulletController;
        }

        private void SetExplosionParticle(ParticleSystem _explosionParticle)
        {
            explosionParticles = _explosionParticle;
            explosionParticles.transform.parent = transform;
            explosionParticles.transform.position = transform.position;
            explosionAudio = explosionParticles.GetComponentInChildren<AudioSource>();
        }

        private void AddVelocity(Vector3 velocityVector)
        {
            rigidBody.velocity = velocityVector;
        }

        // main function -
        // find only tank colliders in a sphere area around the shell and give damage
        // according to its distance away from it.
        private void OnTriggerEnter(Collider other)
        {
            maxTankColliders = bulletController.BulletModel.HitColliders;
            Collider[] hitColliders = new Collider[maxTankColliders];
            int numOfColliders = Physics.OverlapSphereNonAlloc(transform.position, bulletController.BulletModel.ExplosionRadius, hitColliders, bulletController.BulletModel.LayerMask);

            bulletController.CheckHitColliders(hitColliders, numOfColliders, transform.position);
            DestroyBullet();
            explosionParticles = null;
        }

        private void DestroyBullet()
        {
            explosionParticles.transform.parent = null;
            explosionParticles.Play();
            PlayExplosionAudio();
            bulletPool.PushBulletBack(bulletController);
            bulletPool.PushToParticlePool(explosionParticles);
        }

        private void PlayExplosionAudio()
        {
            explosionAudio.enabled = true;
            explosionAudio.Play();
        }
    }
}