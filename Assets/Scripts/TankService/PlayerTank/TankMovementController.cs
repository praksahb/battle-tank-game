using TankBattle.Extensions;
using UnityEngine;

namespace TankBattle.Tank.PlayerTank
{
    public class TankMovementController : MonoBehaviour
    {
        [SerializeField] private InputSystem.InputReader input;
        private TankController tankController;

        private Vector2 _moveDirection;
        private bool _isJumping;

        private Rigidbody rigidBody;

        private void OnEnable()
        {
            SubscribeInputEvents();
        }

        private void Start()
        {
            tankController = PlayerService.Instance.GetTankController();
            if (tankController == null)
            {
                Debug.Log("ERROR");
            }
        }

        private void OnDisable()
        {
            UnsubscribeInputEvents();
        }

        private void SubscribeInputEvents()
        {
            input.MoveEvent += HandleMove;
            input.JumpEvent += HandleJump;
            input.JumpCancelledEvent += HandleCancelledJump;
        }

        private void UnsubscribeInputEvents()
        {
            input.MoveEvent -= HandleMove;
            input.JumpEvent -= HandleJump;
            input.JumpCancelledEvent -= HandleCancelledJump;
        }
        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            if (_moveDirection != Vector2.zero)
            {
                MoveRotate(_moveDirection);
            }
        }

        private void Jump()
        {
            if (_isJumping)
            {
                PerformJump();
            }
        }

        private void HandleCancelledJump()
        {
            _isJumping = false;
        }

        private void HandleJump()
        {
            _isJumping = true;
        }

        private void HandleMove(Vector2 dir)
        {
            _moveDirection = dir;
        }

        //Movement-related physics logic
        private void MoveRotate(Vector2 _moveDirection)
        {
            Vector3 directionVector = _moveDirection.switchYAndZValues();
            PerformMove(directionVector);
            Rotate(directionVector);
        }

        private void PerformMove(Vector3 moveDirection)
        {
            if (!rigidBody)
            {
                rigidBody = tankController.TankView.GetRigidbody();
            }
            rigidBody.MovePosition(rigidBody.position + tankController.TankModel.Speed * Time.fixedDeltaTime * moveDirection);
        }

        private void Rotate(Vector3 rotateDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            targetRotation = Quaternion.RotateTowards
            (
                tankController.TankView.transform.localRotation,
                targetRotation,
                tankController.TankModel.RotateSpeed * Time.fixedDeltaTime
            );
            if (!rigidBody)
            {
                rigidBody = tankController.TankView.GetRigidbody();
            }
            rigidBody.MoveRotation(targetRotation);
        }
        public void PerformJump()
        {
            if (!rigidBody)
            {
                rigidBody = tankController.TankView.GetComponent<Rigidbody>();
            }
            rigidBody.AddForce(tankController.TankModel.JumpForce * Time.deltaTime * Vector3.up, ForceMode.Impulse);
        }
    }
}