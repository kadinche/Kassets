using Cysharp.Threading.Tasks;
using Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kassets.UnityComponents
{
    public class TextBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable stringVariable;
        [SerializeField] private Text text;

        private void Start() => stringVariable.BindTo(text);
    }
}