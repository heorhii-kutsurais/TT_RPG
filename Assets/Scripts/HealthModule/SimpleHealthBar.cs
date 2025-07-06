using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace HealthModule
{
    public sealed class SimpleHealthBar : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _content;

        [SerializeField]
        private Image _fillImage;

        public void Activate()
        {
            _content.DOFade(1f, 0.25f);
        }

        public void Deactivate()
        {
            _content.DOFade(0f, 0.25f);
        }

        public void SetNormalizedHealth(float value)
        {
            _fillImage.fillAmount = value;
        }
    }
}
