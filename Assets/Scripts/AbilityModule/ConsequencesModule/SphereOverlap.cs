using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "SphereOverlap", menuName = "Consequences/SphereOverlap", order = 2)]
    public sealed class SphereOverlap : Consequence
    {
        [SerializeField]
        private Stat _radius;

        [SerializeField]
        private List<Consequence> _consequences = new List<Consequence>();

        public override void Execute(AbilityContext context)
        {
            var casterEntity = context.Caster;
            var center = casterEntity.Position;

            var hits = Physics.OverlapSphere(center, _radius.Value);
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
}