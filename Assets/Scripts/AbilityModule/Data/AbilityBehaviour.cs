using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityModule
{
    [CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "AbilityBehaviour", order = 1)]
    public sealed class AbilityBehaviour : ScriptableObject
    {
        [SerializeField]
        private List<AbilityPhase> _abilityPhases = new List<AbilityPhase>();

        public async UniTask Execute(AbilityContext context)
        {
            foreach (var phase in _abilityPhases)
            {
                await phase.Execute(context);
            }
        }
    }
}