using AbilityModule.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(menuName = "Consequences/SpawnAoE")]
    public sealed class SpawnAoE : Consequence
    {
        [SerializeField]
        private AoEZone _aoePrefab;

        [SerializeField]
        private Stat _duration;

        [SerializeField]
        private Stat _tickInterval;

        [SerializeField]
        private Stat _radius;

        [SerializeField]
        private List<Consequence> _enterConsequences = new List<Consequence>();

        [SerializeField]
        private List<Consequence> _exitConsequences = new List<Consequence>();

        public override void Execute(AbilityContext context)
        {
            if (_aoePrefab == null || context.Caster == null)
            {
                Debug.LogError("Prefab or caster is null");
                return;
            }

            var position = context.Caster.Position;
            var aoe = Object.Instantiate(_aoePrefab, position, Quaternion.identity);
            aoe.Initialize(_radius, _duration, _tickInterval, context.Caster);
            aoe.SetEnterConsequences(_enterConsequences);
            aoe.SetExitConsequences(_exitConsequences);
        }
    }
}