using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Consequences
{
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
}