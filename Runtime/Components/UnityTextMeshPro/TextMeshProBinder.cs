using Cysharp.Threading.Tasks;
using Kassets.VariableSystem;
using TMPro;
using UnityEngine;

namespace Kassets.UnityTextMeshPro
{
    public class TextMeshProBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable stringVariable;
        [SerializeField] private TMP_Text text;

        private void Start() => stringVariable.BindTo(text);
    }
}