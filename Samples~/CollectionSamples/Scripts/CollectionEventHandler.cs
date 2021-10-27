using Kadinche.Kassets.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kadinche.Kassets.Collection.Sample
{
    public class CollectionEventHandler : MonoBehaviour
    {
        [SerializeField] private GameEvent _onAddValueClicked;
        [SerializeField] private GameEvent _onAddGridClicked;
        [SerializeField] private GameEvent _onRemoveGridClicked;
        [SerializeField] private IntCollection _intCollection;

        private const int MaxGrid = 8;

        private void Start()
        {
            _onAddValueClicked.Subscribe(UpdateRandomCollectionValue);
            _onAddGridClicked.Subscribe(AddGrid);
            _onRemoveGridClicked.Subscribe(RemoveGrid);
        }

        private void UpdateRandomCollectionValue()
        {
            if (_intCollection.Count == 0)
                return;
            
            var randomIdx = Random.Range(0, _intCollection.Count);
            _intCollection[randomIdx]++;
        }

        private void AddGrid()
        {
            if (_intCollection.Count >= MaxGrid)
                return;
            
            _intCollection.Add(0);
        }

        private void RemoveGrid()
        {
            if (_intCollection.Count == 0)
                return;
            
            _intCollection.RemoveAt(_intCollection.Count - 1);
        }
    }
}