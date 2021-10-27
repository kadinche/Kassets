using System;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.Variable.Sample
{
    public class HealthText : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatVariable _maxHealthVariable;
        [SerializeField] private TMP_Text _healthText;

        private IDisposable _subscription;
        
        private void Start()
        {
            _subscription = _healthVariable.Subscribe(UpdateHealthText);
        }

        private void UpdateHealthText(float value)
        {
            _healthText.text = $"{Mathf.FloorToInt(value)}/{_maxHealthVariable.Value}";
        }
        
        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}