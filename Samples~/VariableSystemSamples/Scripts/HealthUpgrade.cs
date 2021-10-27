using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.Variable.Sample
{
    public class HealthUpgrade : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatVariable _maxHealthVariable;
        [SerializeField] private float _upgradeValue = 50;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(UpgradeMaxHealth);
        }

        private void UpgradeMaxHealth()
        {
            var upgradedVal = _maxHealthVariable.Value + _upgradeValue;
            if (upgradedVal < Mathf.Abs(_upgradeValue))
                return;
            
            var prevVal = _maxHealthVariable.Value;
            _maxHealthVariable.Value = upgradedVal;
            _healthVariable.Value *= _maxHealthVariable / prevVal;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(UpgradeMaxHealth);
        }
    }
}