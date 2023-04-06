using UnityEngine;
using UnityEngine.UI;

/*
 * Base Tank View 
 * Both player and enemy tank are using this
 */

namespace TankBattle.Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankView : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject explosionPrefab;

        // shooting related
        [SerializeField] private Transform fireTransform;
        [SerializeField] private Slider aimSlider;
        [SerializeField] private AudioSource shootingAudio;
        [SerializeField] private AudioClip chargingClip;
        [SerializeField] private AudioClip fireClip;

        private readonly string fireButton = "Fire1";

        // miscellaneous
        private MeshRenderer[] renderersOnTank;
        private Rigidbody rb;
        private AudioSource explosionAudio;
        private ParticleSystem explosionParticles;

        private TankController tankController;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            renderersOnTank = GetComponentsInChildren<MeshRenderer>();
        }

        private void Update()
        {
            if (tankController.TankModel.TankTypes == TankType.Player)
            {
                TakeInputPress();
            }
        }

        // Getters and Setters
        public virtual void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }
        public TankController GetTankController()
        {
            return tankController;
        }
        public void SetColorOnAllRenderers(Color color)
        {
            for (int i = 0; i < renderersOnTank.Length; i++)
            {
                renderersOnTank[i].material.color = color;
            }
        }
        public Rigidbody GetRigidbody()
        {
            return rb;
        }

        public Transform GetFireTransform()
        {
            return fireTransform;
        }

        public void InstantiateOnDeath()
        {
            explosionParticles = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
            explosionAudio = explosionParticles.GetComponent<AudioSource>();
            OnDeathHandler();
        }

        private void OnDeathHandler()
        {
            explosionParticles.transform.parent = null;
            explosionParticles.Play();
            explosionAudio.Play();
            Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
            Destroy(gameObject);
        }

        public void Damage(Vector3 impactPosition, float _explosionRadius, float _MaxDamage)
        {
            tankController.TakeDamage(impactPosition, _explosionRadius, _MaxDamage);
        }

        // Shooting related UI
        // has to be implemented using the new input system

        private void TakeInputPress()
        {
            aimSlider.value = tankController.TankModel.MinLaunchForce;

            if (tankController.CurrentLaunchForce >= tankController.TankModel.MaxLaunchForce && !tankController.IsFired)
            {
                // at max charge, not fired yet
                tankController.CurrentLaunchForce = tankController.TankModel.MaxLaunchForce;
                tankController.Fire();
            }
            else if (Input.GetButtonDown(fireButton))
            {
                // when fire button is pressed for first time
                tankController.IsFired = false;
                tankController.CurrentLaunchForce = tankController.TankModel.MinLaunchForce;
                //play charging sound
                shootingAudio.clip = chargingClip;
                shootingAudio.Play();
            }
            else if (Input.GetButton(fireButton) && !tankController.IsFired)
            {
                // holding fire button, not fired yet
                tankController.CurrentLaunchForce += tankController.ChargeSpeed * Time.deltaTime;
                aimSlider.value = tankController.CurrentLaunchForce;
            }
            else if (Input.GetButtonUp(fireButton) && !tankController.IsFired)
            {
                // button released, change isFired here
                tankController.Fire();
            }
        }

        public void PlayFiredSound()
        {
            shootingAudio.clip = fireClip;
            shootingAudio.Play();
        }
    }
}