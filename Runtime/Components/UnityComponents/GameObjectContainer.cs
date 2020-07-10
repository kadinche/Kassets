using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class GameObjectContainer : MonoBehaviour
    {
        [SerializeField] private GameObjectVariable _gameObjectVariable;
        [SerializeField] private bool _destroyObjectOnDestroy;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _gameObjectVariable.Subscribe(HandleGameObject, token);
        }

        private void HandleGameObject(GameObject obj)
        {
            if (obj != null)
                _gameObjectVariable.Value.transform.SetParent(transform, false);
        }

        private void OnDestroy()
        {
            if (!_destroyObjectOnDestroy)
                // release parent so game object won't get destroyed along with parent
                _gameObjectVariable.Value.transform.SetParent(null);
            else
                // game object will be destroyed along the parent, hence set to null.
                _gameObjectVariable.Value = null;
        }
    }
}