using DG.Tweening;
using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "RectOverlap", menuName = "Consequences/RectOverlap", order = 1)]
    public sealed class RectOverlap : Consequence
    {
        // Test-only visualization. In production, consider using proper VFX, shaders, or custom gizmo systems.
        [SerializeField]
        private GameObject _testVisualZone;

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

            TestVisualization(center);

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

        /// <summary>
        /// Test-only visualization. In production, consider using proper VFX, shaders, or custom gizmo systems.
        /// </summary>
        private void TestVisualization(Vector3 center)
        {
            var testVisualZone = Instantiate(_testVisualZone, center, Quaternion.identity, null);
            testVisualZone.transform.localScale = new Vector3(_width.Value, _height.Value, _length.Value);
            Destroy(testVisualZone, 0.25f);
        }

        private bool IsHitable(Entity a, Entity b)
        {
            return a.Alignment != b.Alignment;
        }
    }
}