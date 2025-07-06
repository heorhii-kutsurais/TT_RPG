using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using EntityModule;
using Helpers;
using UnityEngine;

namespace AbilityModule
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 1)]
    public sealed class Ability : ScriptableObject
    {
        [SerializeField]
        private Stat _cooldown;

        [SerializeField]
        private AbilityBehaviour _abilityBehaviour;

        [SerializeField]
        private KeyCode _triggerKey;

        [SerializeField]
        private SerializableGuid _guid;

        public KeyCode TriggerKey => _triggerKey;
        public float Cooldown => _cooldown.Value;

        public async UniTask Execute(AbilityContext context)
        {
            await _abilityBehaviour.Execute(context);
        }
    }
}