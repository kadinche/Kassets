using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.UnityComponents
{
    public class InputFieldBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable _stringVariable;
        [SerializeField] private InputField _inputField;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _stringVariable.BindTo(_inputField, (field, s) => field.text = s, token);
            _inputField.OnEndEditAsAsyncEnumerable(token).Subscribe(s => _stringVariable.Value = s).AddTo(token);
        }
    }
}