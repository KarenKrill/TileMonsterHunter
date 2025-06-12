using UnityEngine;
using Zenject;

namespace KarenKrill.Movement
{
    using TileMonsterHunter.Input.Abstractions;

    public class CharacterMoveController : OrdinaryMoveBehaviour
    {
        [Inject]
        public void Initialize(IInputActionService inputActionService)
        {
            _inputActionService = inputActionService;
        }

        [SerializeField]
        private float _jumpHeight = 2.0f, _jumpHorizontalSpeed = 3.0f;
        [SerializeField]
        private float _jumpButtonGracePeriod = 0.2f;

        private IInputActionService _inputActionService;
        private bool _isJumpPressed = false;

        protected override void Awake()
        {
            base.Awake();
            SpeedModifier = 0.5f;
            GravityModifier = 0.5f;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            _inputActionService.Sprint += OnRun;
            _inputActionService.SprintCancel += OnRunCancel;
            _inputActionService.Jump += OnJump;
            _inputActionService.JumpCancel += OnJumpCancel;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputActionService.Sprint -= OnRun;
            _inputActionService.SprintCancel -= OnRunCancel;
            _inputActionService.Jump -= OnJump;
            _inputActionService.JumpCancel -= OnJumpCancel;
        }

        protected override void Update()
        {
            base.Update();
            MoveDirection = new Vector3(_inputActionService.LastMoveDelta.x, 0, _inputActionService.LastMoveDelta.y);
            LookDirection = _inputActionService.LastLookDelta;
            if (IsPulsedUp)
            {
                if (!_isJumpPressed && IsFalling) // ���� �������� �������
                {
                    GravityModifier = 1f; // �������� ������
                }
                else
                {
                    GravityModifier = 0.5f;
                }
            }
        }
        private void OnRun()
        {
            SpeedModifier = 1f;
        }
        private void OnRunCancel()
        {
            SpeedModifier = 0.5f;
        }
        private void OnJump()
        {
            PulseUp(_jumpHeight, _jumpButtonGracePeriod, _jumpHorizontalSpeed);
            _isJumpPressed = true;
        }
        private void OnJumpCancel()
        {
            _isJumpPressed = false;
        }
    }
}