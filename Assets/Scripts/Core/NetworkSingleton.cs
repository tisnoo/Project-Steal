
using Unity.Netcode;
using UnityEngine;

namespace Scripts
{
    public class NetworkSingleton<T> : NetworkBehaviour where T : Component
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                var objects = FindObjectsOfType(typeof(T));
                if (objects is T[] typeComponents)
                {
                    if (typeComponents.Length > 0)
                        _instance = typeComponents[0];

                    if (typeComponents.Length > 1)
                        Debug.LogError("More than one instance :" + typeof(T).Name + " exist in thr scene.");
                }
                else
                {
                    if (_instance == null)
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return _instance;
            }
        }
    }
}