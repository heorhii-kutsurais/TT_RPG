using AbilityModule.StatusModule;
using Helpers;
using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "BurnConsequence", menuName = "Consequences/BurnConsequence")]
    public sealed class BurnConsequence : StatusConsequence
    {
        [SerializeField]
        private Stat _duration;

        [SerializeField]
        private Stat _tickInterval;

        [SerializeField]
        private Stat _damagePerTick;

        public override void Execute(AbilityContext context)
        {
            var target = context.CurrentTarget;
            if (target == null)
            {
                return;
            }

            var burn = new Burn(Guid, _duration.Value, _tickInterval.Value, _damagePerTick.Value);
            target.ApplyStatus(burn);
        }
    }
}