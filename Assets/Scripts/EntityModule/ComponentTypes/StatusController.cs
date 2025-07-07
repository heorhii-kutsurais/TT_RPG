using AbilityModule.StatusModule;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EntityModule.ComponentTypes
{
    public sealed class StatusController : EntityComponent
    {
        private readonly Dictionary<string, Status> _activeStatuses = new Dictionary<string, Status>();

        private Entity _entity;

        public override void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public override void ApplyStatus(Status status)
        {
            var guid = status.Guid;

            if (_activeStatuses.TryGetValue(guid, out var existing))
            {
                existing.OnRefresh();
            }
            else
            {
                status.OnEnd += RemoveStatus;
                status.OnApply(_entity);
                _activeStatuses.Add(guid, status);
            }
        }

        public override void UpdateLogic(float deltaTime)
        {
            var statuses = _activeStatuses.Values.ToArray();
            foreach (var item in statuses)
            {
                item.UpdateLogic(deltaTime);
            }
        }

        private void RemoveStatus(Status status)
        {
            status.OnEnd -= RemoveStatus;
            _activeStatuses.Remove(status.Guid);
        }
    }
}
