using UnityEngine;

namespace AbilityModule
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Stat", order = 1)]
    public sealed class Stat : ScriptableObject
    {
        [SerializeField]
        private float _value;

        public float Value => _value;
    }
}