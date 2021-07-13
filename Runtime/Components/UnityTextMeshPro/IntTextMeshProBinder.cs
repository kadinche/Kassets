using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.VariableSystem;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.UnityTextMeshPro
{
    public class IntTextMeshProBinder : MonoBehaviour
    {
        [SerializeField] private IntVariable _intVariable;
        [SerializeField] private TMP_Text _text;
        [Tooltip("Value as when to hide the text")]
        [SerializeField] private int _hideValue = -999;

        private void Start()
        {
            _intVariable.BindTo(_text);
            _intVariable.Subscribe(value => _text.enabled = value != _hideValue,
                this.GetCancellationTokenOnDestroy());
        }
    }
}