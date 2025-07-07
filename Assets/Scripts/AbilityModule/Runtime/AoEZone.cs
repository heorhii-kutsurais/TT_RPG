using AbilityModule.Consequences;
using EntityModule;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule.Runtime
{
    public abstract class AoEZone : MonoBehaviour
    {
        public abstract void Initialize(Stat radius, Stat duration, Stat tickInterval, Entity caster);
        public virtual void SetEnterConsequences(List<Consequence> consequences) { }
        public virtual void SetExitConsequences(List<Consequence> consequences) { }
    }
}
