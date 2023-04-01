using System.Collections.Generic;
using UnityEngine;

/*
 * Stack works better while firing multiple projectiles
 * However the bug still persists, 
 * wrong explosion particles can go off
 * when some other particle should be played
 * Probable cause: when using list it doesnt remove the item from its list
 * and can be sending item which is already being used 
 */

namespace TankBattle.Services
{
    public class GenericPooling<T> where T : Component
    {
        private T item;
        private int poolLength;
        private Stack<T> poolList;

        public GenericPooling(int poolLength, T item, Transform parentTransform)
        {
            this.item = item;
            initializePool(poolLength, parentTransform);
        }

        private void initializePool(int poolLength, Transform parent)
        {
            poolList = new Stack<T>();
            this.poolLength = poolLength;
            for (int i = 0; i < poolLength; i++)
            {
                T newItem = NewItem();
                newItem.transform.parent = parent;
                poolList.Push(newItem);
            }
        }

        private T NewItem()
        {

            T newItem = Object.Instantiate(item);
            poolList.Push(newItem);
            return newItem;
        }

        public T GetItem()
        {
            if(poolList.Count > 0)
            {
                return poolList.Pop();
            }
            return NewItem();
        }

        public void FreeItem(T item)
        {
                poolList.Push(item);
        }
    }
}
