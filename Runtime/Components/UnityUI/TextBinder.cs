using Cysharp.Threading.Tasks;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kadinche.Kassets.UnityComponents
{
    public class TextBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable stringVariable;
        [SerializeField] private Text text;

        private void Start() => stringVariable.BindTo(text);
    }
}