using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Base Tank View 
 * Both player and enemy tank are using this
 * 
 * Changes To Make: Ideal Case
 * Better would be to create two TankView Scripts which would be inheriting from a base TankView to make it more modular and to increase SRP
 *  Reason to do this is more separating state logic from player tank 
 *  
 *  Realistic Case: Just separate the State logic and make it inherit TankView 
 *  that way only attach it to enemy tank
 */

namespace TankBattle.Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankView : MonoBehaviour
    {
        // health related
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image fillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;
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
        private float maxHealth;

        private TankController tankController;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            renderersOnTank = GetComponentsInChildren<MeshRenderer>();
        }

        private void Update()
        {
            if(tankController.TankModel.TankTypes == TankType.Player)
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
        public void SetMaxHealth(float _maxHealth)
        {
            maxHealth = _maxHealth;
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

        // Health UI related

        public void SetHealthUI()
        {
            healthSlider.value = tankController.TankModel.Health;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankController.TankModel.Health / maxHealth);
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
            //gameObject.SetActive(false);
            Destroy(gameObject);
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