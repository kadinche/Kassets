using System;
using UnityEngine;

namespace Kadinche.Kassets.Variable.Sample
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatVariable _maxHealthVariable;
        [SerializeField] private RectTransform _healthBar;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _healthVariable.Subscribe(UpdateHealth);
        }

        private void UpdateHealth(float value)
        {
            var scale = _healthBar.localScale;
            scale.x = value / _maxHealthVariable;
            _healthBar.localScale = scale;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}