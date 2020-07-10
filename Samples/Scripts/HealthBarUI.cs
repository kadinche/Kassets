using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kassets.Samples
{
    public class HealthBarUI : MonoBehaviour
    {
        public FloatVariable health;
        public FloatVariable maxHealth;
        public Image healthBar;

        public void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            health.Subscribe(UpdateHealth).AddTo(token);
        }

        private void UpdateHealth(float newValue)
        {
            var rt = healthBar.rectTransform;
            var sizeDelta = rt.sizeDelta;
                sizeDelta.x = 2 * (newValue / maxHealth);

            rt.sizeDelta = sizeDelta;
        }
    }
}