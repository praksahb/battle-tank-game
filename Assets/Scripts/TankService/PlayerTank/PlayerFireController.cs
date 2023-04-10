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

        private void Update()
        {
            MoveTurret2();
        }

        private void MoveTurret()
        {
            Vector3 mousePos = MousePosToWorldSpace();

            tankTurret.transform.LookAt(new Vector3(mousePos.x, tankTurret.transform.position.y, mousePos.z));
        }

        private void MoveTurret2()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, transform.position);
            if (p.Raycast(mouseRay, out float hitDist))
            {
                Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                tankTurret.transform.LookAt(new Vector3(hitPoint.x, tankTurret.transform.position.y, hitPoint.z));
            }
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        private Vector3 MousePosToWorldSpace()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.y));
            return Worldpos;
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
