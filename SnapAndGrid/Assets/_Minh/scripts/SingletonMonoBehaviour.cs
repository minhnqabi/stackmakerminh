using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
	protected static T instance;
	public bool preInstantiate;
    public bool isDontDestroy;

	public static T Instance {
		get {
			if (instance == null) {
                //instance = new GameObject ().AddComponent<T> ();
                //instance.gameObject.name = instance.GetType ().Name;
                return null;
			}
			return instance;
		}
	}

	private void Awake () {
		if (preInstantiate)
			instance = this as T;
        if(isDontDestroy)
        {
            DontDestroyOnLoad(this);
        }
	}

	private void Reset () {
		preInstantiate = true;
	}

	public static bool Exists () {
		return (instance != null);
	}
}