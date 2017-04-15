using UnityEngine;

public class TestSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public bool isDestroyWithScene = false;
    //private static object _lock = new object();
    


    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            //lock (_lock)
            {

                return _instance;
            }
        }
    }

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<T>();

            }
            var _currentSingleton = _instance as TestSingleton<T>;
            //var _temp = _instance.gameObject.GetComponent<TestSingleton<T>>();
            _instance.gameObject.name = "(singleton) " + typeof(T).ToString();
            if (!_currentSingleton.isDestroyWithScene)
            {
                DontDestroyOnLoad(_instance.gameObject);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += SingletoneOn;
                UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SingletoneOff;
            }
        }
    }

    private void SingletoneOn(UnityEngine.SceneManagement.Scene hz, UnityEngine.SceneManagement.LoadSceneMode hz2)
    {
        applicationIsQuitting = false;
    }

    private void SingletoneOff(UnityEngine.SceneManagement.Scene hz)
    {
        applicationIsQuitting = true;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SingletoneOn;
        UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= SingletoneOff;
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        var _currentSingleton = _instance as TestSingleton<T>;
        if (!_currentSingleton.isDestroyWithScene)
        applicationIsQuitting = true;
    }

    
}