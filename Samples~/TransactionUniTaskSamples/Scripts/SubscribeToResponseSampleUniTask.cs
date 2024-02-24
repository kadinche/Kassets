using Kadinche.Kassets.Transaction;
using TMPro;
using UnityEngine;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToResponseSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatTransaction _dummyProcessTransaction;
        [SerializeField] private TMP_Text _label;

#if KASSETS_UNITASK
        private void Start() => Begin().Forget();

        private async UniTaskVoid Begin()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while (!token.IsCancellationRequested)
            {
                var response = await _dummyProcessTransaction.WaitForResponse(token);
                _label.text = $"Response received. Response value: {response}";
            }
        }
#else
        private void Start()
        {
            Debug.LogError("UniTask not found. Please import UniTask first");
        }
#endif
    }
}