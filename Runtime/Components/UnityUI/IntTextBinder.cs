using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.UnityComponents
{
    public class IntTextBinder : MonoBehaviour
    {
        [SerializeField] private IntVariable _intVariable;
        [SerializeField] private Text _text;
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