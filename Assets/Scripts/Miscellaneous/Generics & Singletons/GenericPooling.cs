using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle.Services
{
    public class GenericPooling<T> where T : Component
    {
        [SerializeField] protected T item;
        private List<T> poolList;

        public GenericPooling()
        {
            poolList = new List<T>();
        }

        public virtual T NewItem()
        {
            GameObject go = new GameObject();
            T newItem = go.AddComponent<T>();
            poolList.Add(newItem);
            return newItem;
        }

        public virtual T GetItem()
        {
            if(poolList.Count > 0)
            {
                return poolList[poolList.Count - 1];
            }
            return NewItem();
        }

        public virtual void FreeItem(T item)
        {
            poolList.Add(item);
        }
    }
}
