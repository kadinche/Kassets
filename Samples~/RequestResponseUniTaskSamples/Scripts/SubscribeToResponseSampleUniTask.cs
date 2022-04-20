using TMPro;
using UnityEngine;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToResponseSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private TMP_Text _label;

        private void Start() => Begin().Forget();

        private async UniTaskVoid Begin()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while (!token.IsCancellationRequested)
            {
                var response = await _dummyProcessRequestResponseEvent.WaitForResponse(token);
                _label.text = $"Response received. Response value: {response}";
            }
        }
    }
}