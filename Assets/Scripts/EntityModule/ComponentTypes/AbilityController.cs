using AbilityModule;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace EntityModule.ComponentTypes
{
    public sealed class AbilityController : EntityComponent
    {
        [SerializeField]
        private List<AbilityCooldownPanel> _abilityCooldownPanels;

        [SerializeField]
        private Ability[] _abilities;

        private Entity _entity;
        private readonly Dictionary<KeyCode, Ability> _abilityMap = new();
        private readonly Dictionary<Ability, float> _cooldowns = new();

        public override void OnDeath() { }

        public override void UpdateLogic(float deltaTime)
        {
            var cooldowns = _cooldowns.Keys.ToArray();

            for (int i = 0; i < cooldowns.Length; i++)
            {
                var ability = cooldowns[i];

                if (_cooldowns[ability] > 0f)
                {
                    _cooldowns[ability] -= deltaTime;
                    if (_cooldowns[ability] < 0f)
                    {
                        _cooldowns[ability] = 0f;
                    }
                }

                if (i < _abilityCooldownPanels.Count)
                {
                    var fill = Mathf.Clamp01(_cooldowns[ability] / Mathf.Max(ability.Cooldown, 0.01f));

                    _abilityCooldownPanels[i].SetFillNormalized(fill);
                }
            }

            foreach (var pair in _abilityMap)
            {
                var ability = pair.Value;

                if (Input.GetKeyDown(pair.Key))
                {
                    if (_cooldowns[ability] > 0f)
                    {
                        Debug.Log($"Ability '{ability.name}' is on cooldown ({_cooldowns[ability]:0.00}s left).");
                        continue;
                    }

                    var context = new AbilityContext
                    {
                        Caster = _entity
                    };

                    ability.Execute(context).Forget();
                    _cooldowns[ability] = ability.Cooldown;
                }
            }
        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();

            if (_abilities == null || _abilities.Length == 0)
            {
                Debug.LogWarning("AbilityController has no abilities assigned.");
                return;
            }

            for (int i = 0; i < _abilities.Length; i++)
            {
                var ability = _abilities[i];

                if (!_abilityMap.ContainsKey(ability.TriggerKey))
                {
                    _abilityMap.Add(ability.TriggerKey, ability);
                    _cooldowns.Add(ability, 0f);
                }
                else
                {
                    Debug.LogError($"Duplicate key {ability.TriggerKey} in AbilityController");
                }
            }
        }
    }
}
