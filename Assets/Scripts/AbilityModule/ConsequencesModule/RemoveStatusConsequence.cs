using UnityEngine;

namespace AbilityModule.Consequences
{
    [CreateAssetMenu(fileName = "RemoveStatusConsequence", menuName = "Consequences/RemoveStatusConsequence")]
    public sealed class RemoveStatusConsequence : Consequence
    {
        [SerializeField]
        private StatusConsequence _toRemove;

        public override void Execute(AbilityContext context)
        {
            var target = context.CurrentTarget;
            if (target == null)
            {
                return;
            }

            target.RemoveStatus(_toRemove.Guid);
        }
    }
}