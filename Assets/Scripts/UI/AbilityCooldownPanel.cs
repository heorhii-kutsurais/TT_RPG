using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class AbilityCooldownPanel : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        public void SetFillNormalized(float normalized)
        {
            _fillImage.fillAmount = normalized;
        }
    }
}
