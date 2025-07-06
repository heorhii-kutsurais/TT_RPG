using UnityEngine;

namespace EntityModule
{
    public abstract class EntityComponent : MonoBehaviour
    {
        public virtual void UpdateLogic() { }
        public virtual void OnDeath() { }
    }
}
