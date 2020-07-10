using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.Samples
{
    public class TriggerAreaDamage : MonoBehaviour
    {
        public FloatVariable health;
        public float damagePerSecond;
        public string tagToDamage = "Player";

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(tagToDamage)) return;

            health.Value -= damagePerSecond * Time.deltaTime;
        }
    }
}