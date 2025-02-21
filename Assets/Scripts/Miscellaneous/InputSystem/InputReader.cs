using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Main Function: 
 * To handle the functions/actions/events sent by interfaces created from GameInput Action Map
 * 
 */
namespace TankBattle.Tank.PlayerTank.InputSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/InputReader", fileName ="InputReader")]
    public class InputReader : ScriptableObject, GameInputMap.IGameplayActions, GameInputMap.IUIActions
    {
        private GameInputMap gameInput;

        private void OnEnable()
        {
            if(gameInput == null)
            {
                gameInput = new GameInputMap();
                gameInput.Gameplay.SetCallbacks(this);
                gameInput.UI.SetCallbacks(this);
            }

            SetGameplay();
        }

        public void SetGameplay()
        {
            gameInput.Gameplay.Enable();
            gameInput.UI.Disable();
        }
        public void SetUI()
        {
            gameInput.Gameplay.Disable();
            gameInput.UI.Enable();
        }

        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> LookEvent;

        public event Action FireEventPressed;
        public event Action FireEventReleased;

        public event Action JumpEvent;
        public event Action JumpCancelledEvent;

        public event Action PauseEvent;
        public event Action ResumeEvent;

        public void OnMove(InputAction.CallbackContext context)
        {
            // Sending direct vector2 value for movement displacement vector
            
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
            }
            if(context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                ResumeEvent?.Invoke();
                SetGameplay();
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                FireEventPressed?.Invoke();
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                FireEventReleased?.Invoke();
            }
        }
    }
}
