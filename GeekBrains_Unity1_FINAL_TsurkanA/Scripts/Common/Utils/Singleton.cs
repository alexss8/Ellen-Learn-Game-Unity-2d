using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Component
{
    #region Fields

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Making sure there is not other instances of the same type in memory.
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    // Making sure there is not other instances of the same type in memory.
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public static bool IsQuitting;

    private static T _instance;

    #endregion


    #region UnityMethods
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            // If null, this instance is now the Singleton instance of the assigned type.
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy current instance because it must be a duplicate.
            Destroy(gameObject);
        }
    }

    #endregion
}
