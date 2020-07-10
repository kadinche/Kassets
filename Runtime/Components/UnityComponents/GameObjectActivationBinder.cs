using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class GameObjectActivationBinder : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private BoolVariable _activationFlag;
        [SerializeField] private bool _reverseFlag;

        private void Start() => InitializeBind();

        private void InitializeBind()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _target.BindActivation(_activationFlag, _reverseFlag, token);
        }
    }
}