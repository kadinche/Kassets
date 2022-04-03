using System;
using Kadinche.Kassets.Variable;
using UnityEngine;

namespace Kadinche.Kassets.Sample
{
    [Serializable]
    public class Enemy
    {
        public string name;
        public int maxHP;
        public int atk;
        public int def;
    }
    
    [CreateAssetMenu(fileName = "AddToScriptableObjectTest", menuName = MenuHelper.DefaultOtherMenu + "AddToScriptableObjectTest", order = 100)]
    public class AddToScriptableObjectTest : VariableCore<Enemy>
    {
        [SerializeField] private IntVariable _enemyMaxHPVariable;
        [SerializeField] private IntVariable _enemyHPVariable;
        [SerializeField] private IntVariable _enemyAtkVariable;
        [SerializeField] private IntVariable _enemyDefVariable;

        private void OnValidate()
        {
            Value.name = name;
        }
        
#if UNITY_EDITOR

        private const string MaxHPVariableName = "Max HP";
        private const string HPVariableName = "HP";
        private const string AtkVariableName = "Attack";
        private const string DefVariableName = "Defense";

        // Add
        [ContextMenu("Add" + MaxHPVariableName + " Variable", false)]
        private void AddEnemyMaxHPVariable() => _enemyMaxHPVariable = this.Add<IntVariable>(MaxHPVariableName);

        [ContextMenu("Add" + MaxHPVariableName + " Variable", true)]
        private bool AddEnemyMaxHPVariableValidate() => this.AddValidate<IntVariable>(MaxHPVariableName);
        
        [ContextMenu("Add" + HPVariableName + " Variable", false)]
        private void AddEnemyHPVariable() => _enemyHPVariable = this.Add<IntVariable>(HPVariableName);

        [ContextMenu("Add" + HPVariableName + " Variable", true)]
        private bool AddEnemyHPVariableValidate() => this.AddValidate<IntVariable>(HPVariableName);

        [ContextMenu("Add" + AtkVariableName + " Variable", false)]
        private void AddEnemyAtkVariable() => _enemyAtkVariable = this.Add<IntVariable>(AtkVariableName);

        [ContextMenu("Add" + AtkVariableName + " Variable", true)]
        private bool AddEnemyAtkVariableValidate() => this.AddValidate<IntVariable>(AtkVariableName);
        
        [ContextMenu("Add " + DefVariableName + " Variable", false)]
        private void AddDefVariable() => _enemyDefVariable = this.Add<IntVariable>(DefVariableName);

        [ContextMenu("Add " + DefVariableName + " Variable", true)]
        private bool AddDefVariableValidate() => this.AddValidate<IntVariable>(DefVariableName);

        // Remove
        [ContextMenu("Remove " + MaxHPVariableName + " Variable", false)]
        private void RemoveEnemyMaxHPVariable()
        {
            this.Remove<IntVariable>(MaxHPVariableName);
            _enemyMaxHPVariable = null;
        }
        
        [ContextMenu("Remove " + MaxHPVariableName + " Variable", true)]
        private bool RemoveEnemyMaxHPVariableValidate() => this.RemoveValidate<IntVariable>(MaxHPVariableName);
        
        [ContextMenu("Remove " + HPVariableName + " Variable", false)]
        private void RemoveEnemyHPVariable()
        {
            this.Remove<IntVariable>(HPVariableName);
            _enemyHPVariable = null;
        }
        
        [ContextMenu("Remove " + HPVariableName + " Variable", true)]
        private bool RemoveEnemyHPVariableValidate() => this.RemoveValidate<IntVariable>(HPVariableName);
        
        [ContextMenu("Remove " + AtkVariableName + " Variable", false)]
        private void RemoveEnemyAtkVariable()
        {
            this.Remove<IntVariable>(AtkVariableName);
            _enemyAtkVariable = null;
        }
        
        [ContextMenu("Remove " + AtkVariableName + " Variable", true)]
        private bool RemoveEnemyAtkVariableValidate() => this.RemoveValidate<IntVariable>(AtkVariableName);
        
        [ContextMenu("Remove " + DefVariableName + " Variable", false)]
        private void RemoveDefVariable()
        {
            this.Remove<IntVariable>(DefVariableName);
            _enemyDefVariable = null;
        }

        [ContextMenu("Remove " + DefVariableName + " Variable", true)]
        private bool RemoveDefVariableValidate() => this.RemoveValidate<IntVariable>(DefVariableName);
        
#endif
    }
}