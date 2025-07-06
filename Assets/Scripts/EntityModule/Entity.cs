using AbilityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EntityModule
{
    [CreateAssetMenu(fileName = "EntityBase", menuName = "EntityBase", order = 1)]
    public sealed class EntityBase : ScriptableObject
    {
        [SerializeField]
        private Entity _entity;

        [SerializeField]
        private Alignment _alignment;

        [SerializeField]
        private Ability[] _abilities;

        public Entity Entity => _entity;
        public Ability[] Abilities => _abilities;
    }

    public sealed class Entity : MonoBehaviour
    {
        public Alignment Alignment { get; private set; }
        public Vector3 Position => _transform.position;

        private Transform _transform;

        public void TakeDamage(float value)
        {

        }

        public void SetAlignment(Alignment alignment)
        {
            Alignment = alignment;
        }

        private void Awake()
        {
            _transform = transform;
        }
    }

    public enum Alignment
    {
        None,
        Neutral,
        Ally,
        Enemy
    }
}
