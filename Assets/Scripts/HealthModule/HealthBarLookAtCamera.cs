using UnityEngine;

namespace HealthModule
{
    public sealed class HealthBarLookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (_cameraTransform == null) return;

            var direction = transform.position - _cameraTransform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
