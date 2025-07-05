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

    }

    public sealed class Entity : MonoBehaviour
    {
        public Vector3 Position => _transform.position;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
