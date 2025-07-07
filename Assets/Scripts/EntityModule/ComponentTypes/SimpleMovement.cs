using UnityEngine;

namespace EntityModule.ComponentTypes
{
    public sealed class SimpleMovement : EntityComponent
    {
        private static readonly string HorizontalAxis = "Horizontal";
        private static readonly string VerticalAxis = "Vertical";

        [SerializeField]
        private float _movementSpeed;

        [SerializeField]
        private float _rotationSpeed;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public override void UpdateLogic(float deltaTime)
        {
            var inputDirection = ReadInput();
            if (inputDirection.sqrMagnitude > 0.01f)
            {
                Move(inputDirection, deltaTime);
                Rotate(inputDirection, deltaTime);
            }
        }

        private Vector3 ReadInput()
        {
            var h = Input.GetAxisRaw(HorizontalAxis);
            var v = Input.GetAxisRaw(VerticalAxis);
            return new Vector3(h, 0f, v).normalized;
        }

        private void Move(Vector3 direction, float deltaTime)
        {
            _transform.position += _movementSpeed * deltaTime * direction;
        }

        private void Rotate(Vector3 direction, float deltaTime)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * deltaTime);
        }
    }
}
