using AbilityModule.Consequences;
using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Runtime
{
    public sealed class AoEZoneRepeatedTrigger : AoEZone
    {
        [SerializeField]
        private Transform _visualRoot;

        private float _radius;
        private float _duration;
        private float _tickInterval;
        private float _timer;
        private float _tickTimer;
        private Entity _caster;

        private readonly List<Consequence> _consequences = new List<Consequence>();

        public override void Initialize(Stat radius, Stat duration, Stat tickInterval, Entity caster)
        {
            _radius = radius.Value;
            _duration = duration.Value;
            _tickInterval = tickInterval.Value;
            _caster = caster;

            if (_visualRoot != null)
            {
                var diameter = _radius * 2f;
                _visualRoot.localScale = new Vector3(diameter, 0.1f, diameter);
            }
        }

        public override void SetEnterConsequences(List<Consequence> consequences)
        {
            _consequences.AddRange(consequences);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            _tickTimer += Time.deltaTime;

            if (_timer >= _duration)
            {
                Destroy(gameObject);
                return;
            }

            if (_tickTimer >= _tickInterval)
            {
                _tickTimer = 0f;

                var hits = Physics.OverlapSphere(transform.position, _radius);
                foreach (var hit in hits)
                {
                    if (!hit.TryGetComponent<Entity>(out var entity))
                    {
                        continue;
                    }

                    var context = new AbilityContext
                    {
                        Caster = _caster,
                        CurrentTarget = entity
                    };

                    foreach (var item in _consequences)
                    {
                        item.Execute(context);
                    }
                }
            }
        }
    }
}
