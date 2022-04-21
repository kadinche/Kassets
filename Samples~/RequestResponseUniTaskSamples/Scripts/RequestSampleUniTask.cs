using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class RequestSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _processValue;
        private TMP_Text _buttonLabel;

#if KASSETS_UNITASK
        private void Awake()
        {
            _buttonLabel = _button.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _button.onClick.AddListener(BeginRequest);
        }

        private void BeginRequest() => BeginRequestAsync().Forget();

        private async UniTaskVoid BeginRequestAsync()
        {
            _button.interactable = false;
            _buttonLabel.text = "Waiting for Response..";
            
            var responseValue = await _dummyProcessRequestResponseEvent.RequestAsync(Random.value * 3f);
            
            _button.interactable = true;
            _buttonLabel.text = "Request";
            _processValue.text = $"Time elapsed: {responseValue}";
        }
#else
        private void Start()
        {
            Debug.LogError("UniTask not found. Please import UniTask first");
        }
#endif
    }
}