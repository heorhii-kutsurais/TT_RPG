using AbilityModule.StatusModule;
using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "HealConsequence", menuName = "Consequences/HealConsequence")]
    public sealed class HealConsequence : StatusConsequence
    {
        [SerializeField]
        private Stat _tickInterval;

        [SerializeField]
        private Stat _healPerTick;

        public override void Execute(AbilityContext context)
        {
            var target = context.CurrentTarget;
            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }
            if (target.Alignment != context.Caster.Alignment)
            {
                return;
            }

            var heal = new Heal(Guid, _tickInterval.Value, _healPerTick.Value);
            target.ApplyStatus(heal);
        }
    }
}