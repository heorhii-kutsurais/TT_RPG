using AYellowpaper.SerializedCollections;
using EntityModule;
using Helpers;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AbilityModule
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 1)]
    public sealed class Ability : ScriptableObject
    {
        [SerializeField]
        private ValueProgression _cooldown;

        [SerializeField]
        private AbilityBehaviour _abilityBehaviour;

        [SerializeField]
        private KeyCode _triggerKey;

        [SerializeField]
        private SerializableGuid _guid;

        public KeyCode TriggerKey => _triggerKey;

        public void Execute(AbilityContext abilityContext)
        {

        }
    }

    [CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "AbilityBehaviour", order = 1)]
    public sealed class AbilityBehaviour : ScriptableObject
    {
        [SerializeField]
        private List<AbilityPhase> _abilityPhases = new List<AbilityPhase>();

        public void Execute(AbilityContext abilityContext)
        {

        }
    }

    [CreateAssetMenu(fileName = "AbilityPhase", menuName = "AbilityPhase", order = 1)]
    public sealed class AbilityPhase : ScriptableObject
    {
        [SerializeField]
        private Stat _duration;

        [SerializedDictionary]
        private SerializedDictionary<float, List<Consequence>> _consequenceTimeline = new SerializedDictionary<float, List<Consequence>>();

        public void Execute(AbilityContext abilityContext)
        {

        }
    }

    public sealed class AbilityContext
    {
        public Entity Caster;
        public Entity CurrentTarget;
    }

    [CreateAssetMenu(fileName = "Consequence", menuName = "Consequence", order = 1)]
    public abstract class Consequence : ScriptableObject
    {
        public abstract void Execute(AbilityContext context);
    }

    [CreateAssetMenu(menuName = "Consequence/DealDamage")]
    public sealed class DealDamage : Consequence
    {
        [SerializeField]
        private Stat _damage;

        public override void Execute(AbilityContext context)
        {
            var target = context.CurrentTarget;
            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }

            var damage = _damage.Value;

            target.TakeDamage(damage);
        }
    }

    [CreateAssetMenu(fileName = "RectOverlap", menuName = "Consequences/RectOverlap", order = 1)]
    public sealed class RectOverlap : Consequence
    {
        [SerializeField]
        private Stat _width;

        [SerializeField]
        private Stat _height;

        [SerializeField]
        private Stat _length;

        [SerializeField]
        private List<Consequence> _consequences = new List<Consequence>();

        public override void Execute(AbilityContext context)
        {
            var casterEntity = context.Caster;
            var center = casterEntity.Position;
            var halfExtents = new Vector3(_width.Value, _height.Value, _length.Value) * 0.5f;

            var hits = Physics.OverlapBox(center, halfExtents);
            var hitTargets = new List<Entity>();

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent<Entity>(out var targetEntity))
                {
                    continue;
                }

                if (IsHitable(casterEntity, targetEntity))
                {
                    hitTargets.Add(targetEntity);
                }
            }

            foreach (var target in hitTargets)
            {
                context.CurrentTarget = target;

                foreach (var consequence in _consequences)
                {
                    consequence.Execute(context);
                }
            }
        }

        private bool IsHitable(Entity a, Entity b)
        {
            return a.Alignment != b.Alignment;
        }
    }

    [CreateAssetMenu(fileName = "Stat", menuName = "Stat", order = 1)]
    public sealed class Stat : ScriptableObject
    {
        [SerializeField]
        private float _value;

        public float Value => _value;
    }
}