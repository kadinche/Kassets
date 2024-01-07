using System;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.Transaction.Sample
{
    public class SubscribeToResponseSample : MonoBehaviour
    {
        [SerializeField] private FloatTransaction _dummyProcessTransaction;
        [SerializeField] private TMP_Text _label;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessTransaction
                .SubscribeToResponse(value => _label.text = $"Response received. Response value: {value}");
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}