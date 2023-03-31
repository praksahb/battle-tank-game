using System.Collections.Generic;
using UnityEngine;

namespace TankBattle.Services
{
    public class GenericPooling<T> where T : Component
    {
        private T item;
        private List<T> poolList;

        public GenericPooling(int poolLength, T item, Transform parentTransform)
        {
            this.item = item;
            initializePool(poolLength, parentTransform);
        }

        private void initializePool(int poolLength, Transform parent)
        {
            poolList = new List<T>();
            for (int i = 0; i < poolLength; i++)
            {
                T newItem = NewItem();
                newItem.transform.parent = parent;
                poolList.Add(newItem);
            }
        }

        protected virtual T NewItem()
        {

            T newItem = Object.Instantiate(item);
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
