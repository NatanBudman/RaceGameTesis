using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
	where T : Component
{
	private static T _instance;
	public static T Instance {
		get {
			/*
			if (_instance == null)
			{
				Debug.LogError("There's no " + typeof(T).ToString() + " in scene " + SceneManager.GetActiveScene().name);
				return null;
			}
			if (_instance == null) {
					GameObject obj = new GameObject ();
					_instance = obj.AddComponent<T> ();
			}*/
			return _instance;
		}
	}
	
	public virtual void Awake ()
	{
		if (_instance == null) {
			_instance = this as T;
			//DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
	}

    
}
