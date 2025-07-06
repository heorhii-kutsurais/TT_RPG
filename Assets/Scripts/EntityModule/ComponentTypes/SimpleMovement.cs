using UnityEngine;

namespace EntityModule.ComponentTypes
{
    public sealed class SimpleMovement : EntityComponent
    {
        [SerializeField]
        private float _movementSpeed;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public override void UpdateLogic()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(h, 0f, v).normalized;

            if (direction.sqrMagnitude > 0.01f)
            {
                _transform.position += _movementSpeed * Time.deltaTime * direction;
            }
        }
    }
}
