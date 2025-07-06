using AbilityModule;
using HealthModule;
using UnityEngine;

namespace EntityModule
{
    public sealed class Entity : MonoBehaviour
    {
        [SerializeField]
        private Alignment _alignment;

        [SerializeField]
        private SimpleHealth _simpleHealth;

        private EntityComponent[] _entityComponents = new EntityComponent[0];

        public Alignment Alignment => _alignment;
        public Vector3 Position => _transform.position;

        private Transform _transform;

        public void TakeDamage(float value)
        {
            _simpleHealth.TakeDamage(value);
        }

        private void Awake()
        {
            _transform = transform;

            _entityComponents = GetComponentsInChildren<EntityComponent>();

            _simpleHealth.Initialize();
            _simpleHealth.OnDeath -= SimpleHealth_OnDeath;
            _simpleHealth.OnDeath += SimpleHealth_OnDeath;
        }

        private void Update()
        {
            foreach (var item in _entityComponents)
            {
                item.UpdateLogic();
            }
        }

        private void SimpleHealth_OnDeath()
        {
            foreach (var item in _entityComponents)
            {
                item.OnDeath();
            }

            Destroy(gameObject);
        }
    }
}
