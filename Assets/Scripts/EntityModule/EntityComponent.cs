using AbilityModule.StatusModule;
using UnityEngine;

namespace EntityModule
{
    public abstract class EntityComponent : MonoBehaviour
    {
        public virtual void Initialize(Entity entity) { }
        public virtual void UpdateLogic(float deltaTime) { }
        public virtual void OnDeath() { }
        public virtual void ApplyStatus(Status status) { }
        public virtual void RemoveStatus(string guid) { }
    }
}
