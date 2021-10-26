using UnityEngine;

namespace Kadinche.Kassets.CommandSystem.Sample
{
    [CreateAssetMenu(fileName = "StringLogCommand", menuName = MenuHelper.DefaultCommandMenu + "StringLogCommand")]
    public class StringLogCommand : CommandCore<string>
    {
        [SerializeField] private string _tag;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _withCounter;

        private int _counter;
        
        public override void Execute(string toPrint)
        {
            var colorString =
                ((byte)(_color.r * 255)).ToString("x2") +
                ((byte)(_color.g * 255)).ToString("x2") +
                ((byte)(_color.b * 255)).ToString("x2");
            Debug.Log($"<color=#{colorString}>{(string.IsNullOrEmpty(_tag) ? "" : $"[{_tag}] ")}{toPrint}{(_withCounter ? $" {_counter++}" : "")}</color>");
        }
    }
}