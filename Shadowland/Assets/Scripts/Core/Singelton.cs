using UnityEngine;

//http://wiki.unity3d.com/index.php/Singleton

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    //private static bool m_ShuttingDown = false;
    //private static object m_Lock = new object();
    private static T m_Instance;
    //private static bool m_InstanceCreated = false;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            //if (m_ShuttingDown)
            //{
            //    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
            //        "' already destroyed. Returning null.");
            //    return null;
            //}

            //lock (m_Lock)

            //{
            if (m_Instance == null)
            {

                // Search for existing instance.
                m_Instance = FindObjectOfType<T>();

                // Create new instance if one doesn't already exist.
                if (m_Instance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();

                    singletonObject.name = typeof(T).Name;
                    m_Instance = singletonObject.AddComponent<T>();
                    singletonObject.transform.SetParent(null);

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                    //m_InstanceCreated = true;
                }

            }

            return m_Instance;
            //}



        }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //private void OnApplicationQuit()
    //{
    //    m_ShuttingDown = true;
    //}


    //private void OnDestroy()
    //{
    //    m_ShuttingDown = true;

    //    // If the instance is destroyed and it was the one created by the singleton,
    //    // reset the static instance reference and the instance created flag
    //    //if (m_Instance == this && m_InstanceCreated)
    //    //{
    //    //    m_Instance = null;
    //    //    m_InstanceCreated = false;
    //    //}
    //}
}
//{
//    private static T m_Instance;

//    /// <summary>
//    /// Access singleton instance through this property.
//    /// </summary>
//    public static T Instance
//    {
//        get
//        {
//            if (m_Instance == null)
//            {
//                m_Instance = FindObjectOfType<T>();

//                if (m_Instance == null)
//                {
//                    GameObject singletonObject = new GameObject();
//                    m_Instance = singletonObject.AddComponent<T>();
//                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
//                    DontDestroyOnLoad(singletonObject);
//                }
//            }

//            return m_Instance;
//        }
//    }

//    private void OnDestroy()
//    {
//        if (m_Instance == this)
//        {
//            m_Instance = null;
//        }
//    }
//}