using AbilityModule;
using AbilityModule.StatusModule;
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

        public void ApplyStatus(Status status)
        {
            foreach (var item in _entityComponents)
            {
                item.ApplyStatus(status);
            }
        }

        public void RemoveStatus(string guid)
        {
            foreach (var item in _entityComponents)
            {
                item.RemoveStatus(guid);
            }
        }

        public void TakeHeal(float value)
        {
            _simpleHealth.TakeHeal(value);
        }

        public void TakeDamage(float value)
        {
            _simpleHealth.TakeDamage(value);
        }

        private void Awake()
        {
            _transform = transform;

            _entityComponents = GetComponentsInChildren<EntityComponent>();

            foreach (var item in _entityComponents)
            {
                item.Initialize(this);
            }

            _simpleHealth.Initialize();
            _simpleHealth.OnDeath -= SimpleHealth_OnDeath;
            _simpleHealth.OnDeath += SimpleHealth_OnDeath;
        }

        private void Update()
        {
            foreach (var item in _entityComponents)
            {
                var deltaTime = Time.deltaTime;
                item.UpdateLogic(deltaTime);
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
