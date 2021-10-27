using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CreateAssetMenu(fileName = "CameraVariable", menuName = MenuHelper.DefaultVariableMenu + "Camera")]
    public class CameraVariable : VariableCore<Camera>
    {
        [SerializeField] private CameraFallback _fallbackType;

        public override Camera Value
        {
            get
            {
                if (base.Value == null && _fallbackType != CameraFallback.Null)
                {
                    switch (_fallbackType)
                    {
                        case CameraFallback.Main:
                            base.Value = Camera.main;
                            break;
                        case CameraFallback.Current:
                            base.Value = Camera.current;
                            break;
                    }
                }

                return base.Value;
            }
            set => base.Value = value;
        }
    }

    internal enum CameraFallback
    {
        Null,
        Main,
        Current
    }
}