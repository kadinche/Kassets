using Cysharp.Threading.Tasks;
using Kadinche.Kassets.VariableSystem;
using Kadinche.Kassets.Utilities;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
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