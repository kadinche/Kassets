using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class RequestSample : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _processValue;
        private TMP_Text _buttonLabel;

        private void Awake()
        {
            _buttonLabel = _button.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _button.onClick.AddListener(BeginRequest);
        }

        private void BeginRequest()
        {
            _button.interactable = false;
            _buttonLabel.text = "Waiting for Response..";
            _dummyProcessRequestResponseEvent.Request(0f, OnResponse);
        }

        private void OnResponse(float responseValue)
        {
            _button.interactable = true;
            _buttonLabel.text = "Request";
            _processValue.text = $"Time elapsed: {responseValue}";
        }
    }
}