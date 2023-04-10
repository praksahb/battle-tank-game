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
        private LineRenderer lineRenderer;


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
            createRendererLine();
        }

        private void OnDisable()
        {
            input.FireEventPressed -= HandleFirePressed;
            input.FireEventReleased -= HandleFireReleased;
            //input.LookEvent -= HandleLook;

        }

        private void Update()
        {
            MoveTurret();
        }

        private void MoveTurret()
        {
            Vector3 mousePos = MousePosToWorldSpace();
            Vector3 objPos = Camera.main.ScreenToWorldPoint(tankTurret.transform.position);

            float angle = AngleBetweenTwoPoints(objPos, mousePos);

            tankTurret.transform.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        private Vector3 MousePosToWorldSpace()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
            return Worldpos;
        }

        private void DrawLine(Vector3 mousePos)
        {
            //For drawing line in the world space, provide the x,y,z values
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0)); //x,y and z position of the starting point of the line
            lineRenderer.SetPosition(1, mousePos); //x,y and z position of the end point of the line
        }

        private void createRendererLine()
        {
            //For creating line renderer object
            lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;

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
