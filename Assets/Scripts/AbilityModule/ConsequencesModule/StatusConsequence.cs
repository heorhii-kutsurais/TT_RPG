using Helpers;
using UnityEngine;

namespace AbilityModule.Consequences
{
    public abstract class StatusConsequence : Consequence
    {
        [SerializeField]
        private SerializableGuid _guid;

        public string Guid => _guid.ToString();
    }
}