using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 RawMovementInput { get; private set; }
        public Vector2 RawLookInput { get; private set; }
        public int RawInputX { get; private set; }
        public int RawInputY { get; private set; }

        public bool JumpInput { get; private set; }
        public bool JumpInputStop { get; private set; }

        [SerializeField] private float _inputHoldTime = 0.2f;

        private float _jumpInputStartTime;

        private void Update()
        {
            CheckJumpInputHoldTime();
        }
        #region Convert InputSystem
        public void OnMoveInput(InputAction.CallbackContext p_context)
        {
            RawMovementInput = p_context.ReadValue<Vector2>();

            RawInputX = Mathf.RoundToInt(RawMovementInput.x);
            RawInputY = Mathf.RoundToInt(RawMovementInput.y);

        }
        public void OnLookInput(InputAction.CallbackContext p_context)
        {
            RawLookInput = p_context.ReadValue<Vector2>();
        }
        public void OnJumpInput(InputAction.CallbackContext p_context)
        {
            if (p_context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (p_context.canceled)
            {
                JumpInputStop = true;
            }
        }
        #endregion
        #region Methods
        public void SetJumpInputToFalse() => JumpInput = false;
        private void CheckJumpInputHoldTime()
        {
            if (Time.time >= _jumpInputStartTime + _inputHoldTime)
            {
                JumpInput = false;
            }
        }
        #endregion
    }

