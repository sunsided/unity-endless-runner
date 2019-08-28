using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class Pool : MonoBehaviour
    {
        public static Pool Singleton;
        public List<Item> items;
        public List<GameObject> pooledItems = new List<GameObject>();

        public GameObject GetRandomItem()
        {
            pooledItems.Shuffle();
            for (var i = 0; i < pooledItems.Count; ++i)
            {
                var item = pooledItems[i];
                if (item.activeInHierarchy) continue;
                return item;
            }

            foreach (var item in items)
            {
                if (!item.expandable) continue;
                var obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }

            return null;
        }

        private void Awake()
        {
            Singleton = this;
            foreach (var item in items)
            {
                for (var i = 0; i < item.amount; ++i)
                {
                    var obj = Instantiate(item.prefab);
                    obj.SetActive(false);
                    pooledItems.Add(obj);
                }
            }
        }

        private void Start()
        {

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
