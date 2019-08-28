using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.Scripts
{
    public class Pool : MonoBehaviour
    {
        public static Pool Singleton;
        public GameObject parentElement;
        public List<Item> items;
        private readonly List<GameObject> _pooledItems = new List<GameObject>();

        public GameObject GetRandomItem()
        {
            _pooledItems.Shuffle();
            for (var i = 0; i < _pooledItems.Count; ++i)
            {
                var item = _pooledItems[i];
                if (item.activeInHierarchy) continue;
                return item;
            }

            foreach (var item in items.Where(item => item.expandable))
            {
                CreateInactiveAndAddToPool(item.prefab);
            }

            return null;
        }

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;
            if (parentElement == null) parentElement = gameObject;
            foreach (var item in items)
            {
                CreateInactiveAndAddToPool(item.prefab, item.amount);
            }
        }

        private void CreateInactiveAndAddToPool([NotNull] GameObject prefab)
        {
            var obj = Instantiate(prefab, parentElement.transform);
            obj.name = prefab.name;
            obj.SetActive(false);
            _pooledItems.Add(obj);
        }

        private void CreateInactiveAndAddToPool([NotNull] GameObject prefab, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                CreateInactiveAndAddToPool(prefab);
            }
        }

        [Serializable]
        public class Item
        {
            /// <summary>
            /// The prefab to use.
            /// </summary>
            public GameObject prefab;

            /// <summary>
            /// The number of prepared instances of that item.
            /// </summary>
            public int amount = 1;

            /// <summary>
            /// Whether or not more instances can be created if we run out of them.
            /// </summary>
            public bool expandable;
        }
    }
}
