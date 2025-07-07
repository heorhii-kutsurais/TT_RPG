using AbilityModule;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace EntityModule.ComponentTypes
{
    public sealed class AbilityController : EntityComponent
    {
        [SerializeField]
        private Ability[] _abilities;

        private Entity _entity;
        private readonly Dictionary<KeyCode, Ability> _abilityMap = new Dictionary<KeyCode, Ability>();

        public override void OnDeath()
        {

        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();

            if (_abilities == null || _abilities.Length == 0)
            {
                Debug.LogWarning("AbilityController has no abilities assigned.");
                return;
            }

            foreach (var ability in _abilities)
            {
                if (!_abilityMap.ContainsKey(ability.TriggerKey))
                {
                    _abilityMap.Add(ability.TriggerKey, ability);
                }
                else
                {
                    Debug.LogError($"Duplicate key {ability.TriggerKey} in AbilityController");
                }
            }
        }

        public override void UpdateLogic(float deltaTime)
        {
            foreach (var pair in _abilityMap)
            {
                if (Input.GetKeyDown(pair.Key))
                {
                    var context = new AbilityContext
                    {
                        Caster = _entity
                    };

                    pair.Value.Execute(context).Forget();
                }
            }
        }
    }
}
