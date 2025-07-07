using AbilityModule.Consequences;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbilityModule
{
    [CreateAssetMenu(fileName = "AbilityPhase", menuName = "AbilityPhase", order = 1)]
    public sealed class AbilityPhase : ScriptableObject
    {
        [SerializeField]
        private Stat _duration;

        [SerializeField, SerializedDictionary("Normalized time (0-1)", "Consequence")]
        private SerializedDictionary<float, List<Consequence>> _consequenceTimeline = new SerializedDictionary<float, List<Consequence>>();
        
        public async UniTask Execute(AbilityContext context)
        {
            Debug.Log($"[AbilityPhase] Start phase {name}");

            float duration = _duration.Value;
            float elapsed = 0f;

            var sortedTimeline = _consequenceTimeline
                .OrderBy(kvp => kvp.Key)
                .ToList();

            int index = 0;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = elapsed / duration;

                while (index < sortedTimeline.Count && normalizedTime >= sortedTimeline[index].Key)
                {
                    var consequences = sortedTimeline[index].Value;
                    foreach (var consequence in consequences)
                    {
                        consequence.Execute(context);
                    }

                    index++;
                }

                await UniTask.Yield();
            }
        }
    }
}