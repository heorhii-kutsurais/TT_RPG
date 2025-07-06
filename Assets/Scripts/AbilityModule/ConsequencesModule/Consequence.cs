using UnityEngine;

namespace AbilityModule.Consequences
{
    public abstract class Consequence : ScriptableObject 
    {
        public abstract void Execute(AbilityContext context);
    }
}