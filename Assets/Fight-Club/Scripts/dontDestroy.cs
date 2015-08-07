using UnityEngine;
using System.Collections;

public class dontDestroy : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

}
