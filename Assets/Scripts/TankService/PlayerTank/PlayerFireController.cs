using TankBattle.Services;
using UnityEngine;

namespace TankBattle.Tank.PlayerTank
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField] private InputSystem.InputReader input;

        [SerializeField] private Transform tankTurret;


        private TankController tankController;

        private Vector2 lookDirection;
        private float rotationSpeed = 10f;

        private void Start()
        {
            tankController = PlayerService.Instance.GetTankController();
            if (tankController == null)
            {
                Debug.Log("ERROR");
            }
        }

        private void OnEnable()
        {
            input.FireEventPressed += HandleFirePressed;
            input.FireEventReleased += HandleFireReleased;
            //input.LookEvent += HandleLook;
        }

        private void OnDisable()
        {
            input.FireEventPressed -= HandleFirePressed;
            input.FireEventReleased -= HandleFireReleased;
            //input.LookEvent -= HandleLook;

        }

        private void FixedUpdate()
        {
            MoveTurretAccurate();
        }

        // Working functions
        private void MoveTurretInAccurate()
        {
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            tankTurret.transform.LookAt(new Vector3(mousePosWorld.x, tankTurret.transform.position.y, mousePosWorld.z + mousePosWorld.y));
        }

        private void MoveTurretAccurate()
        {
            Vector3 mousePos = Input.mousePosition;

            // Convert mouse position to world space using depth buffer
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToViewportPoint(transform.position).z));

            // Set turret rotation to look at world position
            tankTurret.transform.LookAt(new Vector3(worldPos.x, tankTurret.transform.position.y, worldPos.z + worldPos.y));
        }

        private void MoveTurretUsingRaycast()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, transform.position);
            if (p.Raycast(mouseRay, out float hitDist))
            {
                Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                tankTurret.transform.LookAt(new Vector3(hitPoint.x, tankTurret.transform.position.y, hitPoint.z));
            }
        }

        private void HandleFireReleased()
        {
            BulletProjectile.isFirePressed = false;
            tankController.Fire();
        }

        private void HandleFirePressed()
        {
            BulletProjectile.isFirePressed = true;
        }
    }
}
