using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.VariableSystem;
using TMPro;
using UnityEngine;

namespace Kassets.UnityTextMeshPro
{
    public class InputFieldTextMeshProBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable _stringVariable;
        [SerializeField] private TMP_InputField _inputField;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _stringVariable.Subscribe(value => _inputField.text = value).AddTo(token);
            _inputField.OnEndEditAsAsyncEnumerable(token).Subscribe(s => _stringVariable.Value = s).AddTo(token);
        }
    }
}