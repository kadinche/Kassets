using System;
using Kadinche.Kassets.Transaction;
using TMPro;
using UnityEngine;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToRequestSampleUniTask : MonoBehaviour
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