using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.Variable.Sample
{
    public class DataEditor : MonoBehaviour
    {
        [SerializeField] private CustomVariable _customVariable;

        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_InputField _intInputField;
        [SerializeField] private TMP_InputField _floatInputField;
        [SerializeField] private TMP_InputField _stringInputField;
        [SerializeField] private Button[] _intEditButtons;
        [SerializeField] private Slider _floatSlider;

        private IDisposable _subscription;

        private void Start()
        {
            UpdateData(_customVariable);
            _subscription = _customVariable.Subscribe(UpdateData);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(UpdateBoolField);
            _intInputField.onValueChanged.AddListener(UpdateIntField);
            _floatInputField.onValueChanged.AddListener(UpdateFloatField);
            _stringInputField.onValueChanged.AddListener(UpdateStringField);
            _intEditButtons[0].onClick.AddListener(SubtractIntField);
            _intEditButtons[1].onClick.AddListener(AddIntField);
            _floatSlider.onValueChanged.AddListener(UpdateFloatField);
        }
        
        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(UpdateBoolField);
            _intInputField.onValueChanged.RemoveListener(UpdateIntField);
            _floatInputField.onValueChanged.RemoveListener(UpdateFloatField);
            _stringInputField.onValueChanged.RemoveListener(UpdateStringField);
            _intEditButtons[0].onClick.RemoveListener(SubtractIntField);
            _intEditButtons[1].onClick.RemoveListener(AddIntField);
            _floatSlider.onValueChanged.RemoveListener(UpdateFloatField);
        }

        private void UpdateBoolField(bool isOn)
        {
            var v = _customVariable.Value;
            v.boolField = isOn;
            _customVariable.Value = v;
        }

        private void AddIntField() => UpdateIntField(_customVariable.Value.intField + 1);
        private void SubtractIntField() => UpdateIntField(_customVariable.Value.intField - 1);
        private void UpdateIntField(string value) => UpdateIntField(int.Parse(value));
        private void UpdateIntField(int value)
        {
            var v = _customVariable.Value;
            v.intField = value;
            _customVariable.Value = v;
        }
        
        private void UpdateFloatField(string value) => UpdateFloatField(float.Parse(value));
        private void UpdateFloatField(float value)
        {
            var v = _customVariable.Value;
            v.floatField = value;
            _customVariable.Value = v;
        }
        
        private void UpdateStringField(string value)
        {
            var v = _customVariable.Value;
            v.stringField = value;
            _customVariable.Value = v;
        }

        private void UpdateData(CustomStruct data)
        {
            _toggle.SetIsOnWithoutNotify(data.boolField);
            _intInputField.SetTextWithoutNotify(data.intField.ToString());
            _floatInputField.SetTextWithoutNotify(data.floatField.ToString(CultureInfo.InvariantCulture));
            _stringInputField.SetTextWithoutNotify(data.stringField);
            _floatSlider.SetValueWithoutNotify(data.floatField);
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}