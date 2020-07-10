using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.Samples
{
    public class Player : MonoBehaviour
    {
        public FloatVariable health;
        public FloatVariable maxHealth;

        private void Start()
        {
            health.Value = maxHealth.Value;
        }
    }
}
