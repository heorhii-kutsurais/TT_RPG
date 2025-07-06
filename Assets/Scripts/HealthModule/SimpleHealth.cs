using UnityEngine;

namespace HealthModule
{
    public sealed class SimpleHealth : MonoBehaviour
    {
        [SerializeField]
        private SimpleHealthBar _simpleHealthBar;

        [SerializeField]
        private float _max;

        private bool _isDead;
        private float _current;

        public event System.Action OnDeath;

        public void Initialize()
        {
            _isDead = false;

            _current = _max;
            UpdateHealth(_current, _max);

            _simpleHealthBar.Deactivate();
        }

        public void TakeDamage(float amount)
        {
            _simpleHealthBar.Activate();

            if (amount < 0)
            {
                Debug.LogError("Negative damage");
            }

            _current -= amount;

            if (_current <= 0f && !_isDead)
            {
                _isDead = true;
                _current = 0f;
                OnDeath?.Invoke();
            }

            UpdateHealth(_current, _max);
        }

        private void UpdateHealth(float current, float max)
        {
            var normalized = Mathf.Clamp01(current / max);
            _simpleHealthBar.SetNormalizedHealth(normalized);
        }
    }
}
