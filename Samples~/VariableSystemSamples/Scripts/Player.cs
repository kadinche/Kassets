using UnityEngine;

namespace Kadinche.Kassets.Variable.Sample
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private FloatVariable health;
        [SerializeField] private FloatVariable maxHealth;

        private void OnDamaged(float damage)
        {
            // An event is raised on value assign.
            health.Value = Mathf.Clamp(health - damage, 0, maxHealth);
        }
    }
}