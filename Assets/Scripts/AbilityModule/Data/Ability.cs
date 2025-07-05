using AYellowpaper.SerializedCollections;
using EntityModule;
using Helpers;
using System.Collections.Generic;
using UnityEngine;

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
        private KeyCode _key;

        [SerializeField]
        private SerializableGuid _guid;
    }

    [CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "AbilityBehaviour", order = 1)]
    public sealed class AbilityBehaviour : ScriptableObject
    {
        [SerializeField]
        private List<AbilityPhase> _abilityPhases = new List<AbilityPhase>();
    }

    [CreateAssetMenu(fileName = "AbilityPhase", menuName = "AbilityPhase", order = 1)]
    public sealed class AbilityPhase : ScriptableObject
    {
        [SerializeField]
        private Stat _duration;

        [SerializedDictionary]
        private SerializedDictionary<float, List<Consequence>> _consequenceTimeline = new SerializedDictionary<float, List<Consequence>>();
    }

    public sealed class AbilityContext
    {
        public Entity Caster;
    }

    [CreateAssetMenu(fileName = "Consequence", menuName = "Consequence", order = 1)]
    public abstract class Consequence : ScriptableObject
    {
        public abstract void Execute(AbilityContext context);
    }

    [CreateAssetMenu(fileName = "RectOverlap", menuName = "Consequences/RectOverlap", order = 1)]
    public abstract class RectOverlap : Consequence
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
            var caster = context.Caster;
            var center = caster.Position; 
        }
    }

    [CreateAssetMenu(fileName = "Stat", menuName = "Stat", order = 1)]
    public sealed class Stat : ScriptableObject
    {
        [SerializeField]
        private ValueProgression _value;

        public float GetValueAtLevel(int level)
        {
            return _value.GetValueAtLevel(level);
        }
    }
}