using TMPro;
using UnityEngine;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToRequestSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private TMP_Text _label;

        private void Start()
        {
            _dummyProcessRequestResponseEvent
                .SubscribeToRequest(value =>
                {
                    // if (!_label.enabled)
                    //     _label.enabled = true;
                    // else
                        _label.text = $"Request sent. Request value: {value}";
                })
                .AddTo(this.GetCancellationTokenOnDestroy());
        }
    }
}