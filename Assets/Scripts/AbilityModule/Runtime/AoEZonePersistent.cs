using AbilityModule.Consequences;
using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Runtime
{
    public sealed class AoEZonePersistent : AoEZone
    {
        [SerializeField]
        private Transform _visualRoot;

        private float _radius;
        private float _duration;
        private float _tickInterval;
        private float _timer;
        private float _tickTimer;

        private readonly HashSet<Entity> _trackedEntities = new HashSet<Entity>();
        private readonly List<Consequence> _onEnterConsequences = new List<Consequence>();
        private readonly List<Consequence> _onExitConsequences = new List<Consequence>();
        private Entity _caster;

        public override void Initialize(Stat radius, Stat duration, Stat tickInterval, Entity caster)
        {
            _radius = radius.Value;
            _duration = duration.Value;
            _tickInterval = tickInterval.Value;
            _caster = caster;

            if (_visualRoot != null)
            {
                float diameter = _radius * 2f;
                _visualRoot.localScale = new Vector3(diameter, 0.1f, diameter);
            }
        }

        public override void SetEnterConsequences(List<Consequence> consequences)
        {
            _onEnterConsequences.AddRange(consequences);
        }

        public override void SetExitConsequences(List<Consequence> consequences)
        {
            _onExitConsequences.AddRange(consequences);
        }

        private void Update()
        {
            UpdateTimers();

            if (IsExpired())
            {
                Destroy(gameObject);
                return;
            }

            if (!IsTickReady())
            {
                return;
            }

            _tickTimer = 0f;

            var currentEntities = DetectEntitiesInZone();

            HandleEnterConsequences(currentEntities);
            HandleExitConsequences(currentEntities);
        }

        private void UpdateTimers()
        {
            _timer += Time.deltaTime;
            _tickTimer += Time.deltaTime;
        }

        private bool IsExpired()
        {
            return _timer >= _duration;
        }

        private bool IsTickReady()
        {
            return _tickTimer >= _tickInterval;
        }

        private HashSet<Entity> DetectEntitiesInZone()
        {
            var hits = Physics.OverlapSphere(transform.position, _radius);
            var currentEntities = new HashSet<Entity>();

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Entity>(out var entity))
                {
                    currentEntities.Add(entity);
                }
            }

            return currentEntities;
        }

        private void HandleEnterConsequences(HashSet<Entity> currentEntities)
        {
            foreach (var entity in currentEntities)
            {
                var context = new AbilityContext
                {
                    Caster = _caster,
                    CurrentTarget = entity
                };

                foreach (var consequence in _onEnterConsequences)
                {
                    consequence.Execute(context);
                }

                _trackedEntities.Add(entity);
            }
        }

        private void HandleExitConsequences(HashSet<Entity> currentEntities)
        {
            var toRemove = new List<Entity>();

            foreach (var entity in _trackedEntities)
            {
                if (currentEntities.Contains(entity))
                {
                    continue;
                }

                foreach (var consequence in _onExitConsequences)
                {
                    if (consequence is StatusConsequence statusConsequence)
                    {
                        Debug.Log("Worked");
                        entity.RemoveStatus(statusConsequence.Guid);
                    }
                    else
                    {
                        var context = new AbilityContext
                        {
                            Caster = _caster,
                            CurrentTarget = entity
                        };
                        consequence.Execute(context);
                    }
                }

                toRemove.Add(entity);
            }

            foreach (var item in toRemove)
            {
                _trackedEntities.Remove(item);
            }
        }
    }
}
