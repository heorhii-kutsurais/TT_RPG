using EntityModule;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "SphereOverlap", menuName = "Consequences/SphereOverlap", order = 2)]
    public sealed class SphereOverlap : Consequence
    {
        // Test-only visualization. In production, consider using proper VFX, shaders, or custom gizmo systems.
        [SerializeField]
        private GameObject _testVisualZone;

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

            TestVisualization(center);


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
            testVisualZone.transform.localScale = _radius.Value * 2f * Vector3.one;
            Destroy(testVisualZone, 0.35f);
        }

        private bool IsHitable(Entity a, Entity b)
        {
            return a.Alignment != b.Alignment;
        }
    }
}