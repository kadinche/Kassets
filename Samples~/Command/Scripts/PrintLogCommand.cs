using UnityEngine;

namespace Kadinche.Kassets.CommandSystem.Sample
{
    [CreateAssetMenu(fileName = "PrintLogCommand", menuName = MenuHelper.DefaultCommandMenu + "PrintLogCommand")]
    public class PrintLogCommand : CommandBase
    {
        [SerializeField] private string _toPrint = "Debugging...";
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _withCounter;

        private int _counter;
        
        public override void Execute()
        {
            var colorString =
                ((byte)(_color.r * 255)).ToString("x2") +
                ((byte)(_color.g * 255)).ToString("x2") +
                ((byte)(_color.b * 255)).ToString("x2");
            Debug.Log($"<color=#{colorString}>{_toPrint}{(_withCounter ? $" {_counter++}" : "")}</color>");
        }
    }
}