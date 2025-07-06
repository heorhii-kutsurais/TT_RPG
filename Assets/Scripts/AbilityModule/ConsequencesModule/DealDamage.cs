using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "DealDamage", menuName = "Consequences/DealDamage")]
    public sealed class DealDamage : Consequence
    {
        [SerializeField]
        private Stat _damage;

        public override void Execute(AbilityContext context)
        {
            var target = context.CurrentTarget;
            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }

            var damage = _damage.Value;

            target.TakeDamage(damage);
        }
    }
}