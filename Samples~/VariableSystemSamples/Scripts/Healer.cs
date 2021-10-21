using UnityEngine;
using UnityEngine.EventSystems;

namespace Kadinche.Kassets.Variable.Sample
{
    public class Healer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatVariable _maxHealthVariable;
        [SerializeField] private float _healPerSecond;

        private bool _hoverFlag;

        private void Update()
        {
            if (_hoverFlag)
            {
                _healthVariable.Value = Mathf.Clamp(_healthVariable.Value + Time.deltaTime * _healPerSecond, 0, _maxHealthVariable);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverFlag = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoverFlag = false;
        }
    }
}