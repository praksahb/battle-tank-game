using UnityEngine;

namespace TankBattle
{
    // Keeping it non-persistent works for this project.
    // Single Level - restarting dont need any persistent singleton managers.

    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            //// 1.
            //if (Instance == null)
            //{
            //    instance = (T)this;
            //    //DontDestroyOnLoad(this as T);
            //}
            //else
            //{
            //    Destroy(this);
            //}

            // 2.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this as T;
            }
        }
    }
}