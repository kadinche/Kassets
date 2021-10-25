using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.Collection.Sample
{
    public class CollectionViewHandler : MonoBehaviour
    {
        [SerializeField] private IntCollection _intCollection;

        private readonly List<TMP_Text> _texts = new List<TMP_Text>();

        private void Awake()
        {
            GetComponentsInChildren(true, _texts);
        }

        private void Start()
        {
            for (var i = 0; i < _texts.Count; i++)
            {
                _texts[i].transform.parent.gameObject.SetActive(i < _intCollection.Count);
                
                var j = i;
                if (i < _intCollection.Count)
                {
                    _intCollection.SubscribeToValueAt(i, value => _texts[j].text = "" + value);
                }
            }

            _intCollection.SubscribeOnAdd(OnCollectionAdded);
            _intCollection.SubscribeOnRemove(OnCollectionRemoved);
        }

        private void OnCollectionAdded(int addedValue)
        {
            var idx = _intCollection.Count - 1;
            _texts[idx].transform.parent.gameObject.SetActive(true);
            _intCollection.SubscribeToValueAt(idx, value => _texts[idx].text = _texts[idx].text = "" + value);
        }
        
        private void OnCollectionRemoved(int removedValue)
        {
            var idx = _intCollection.Count;
            _texts[idx].transform.parent.gameObject.SetActive(false);
        }
    }
}