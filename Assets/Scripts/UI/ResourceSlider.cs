using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceSlider : MonoBehaviour
    {
        [SerializeField] private Image imageSlider;

        public void UpdateSlider(float current, float max)
        {
            if (imageSlider == null) return;
            var result = Mathf.Lerp(0, 1, current);
            var fillAmount = Mathf.Clamp(current / max, 0, 1);
            imageSlider.fillAmount = fillAmount;
        }
    }
}
