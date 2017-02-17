using UnityEngine;
using System.Collections;
using GDGeek;

public class WebTestIt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var loader = new WebLoaderTask<UserInfo>("http://127.0.0.1:8080/");
		loader.pack.addField("name", "name");
		loader.pack.addField("password", "password");
		bool error = false;
		loader.onSucceed += delegate(UserInfo info) {
			Debug.Log("succeed");
			error = false;
		};
		loader.onError += delegate(string msg) {
			Debug.Log("error" + msg);
			error = true;
		};
		TaskManager.Run (loader);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
