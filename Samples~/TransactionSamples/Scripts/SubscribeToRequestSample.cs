using System;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.Transaction.Sample
{
    public class SubscribeToRequestSample : MonoBehaviour
    {
        [SerializeField] private FloatTransaction _dummyProcessTransaction;
        [SerializeField] private TMP_Text _label;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessTransaction
                .SubscribeToRequest(value => _label.text = $"Request sent. Request value: {value}");
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}