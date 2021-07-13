using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.UnityComponents
{
    public class ToggleBinder : MonoBehaviour
    {
        [SerializeField] private BoolVariable toggleVariable;
        [SerializeField] private Toggle toggle;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            toggleVariable.Subscribe(value => toggle.SetIsOnWithoutNotify(value)).AddTo(token);
            toggle.OnValueChangedAsAsyncEnumerable(token).Subscribe(value => toggleVariable.Value = value).AddTo(token);
        }
    }
}