using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class MessageReceive : MonoBehaviour {
	private Stack<String> messages_ = new Stack<String>();
	public Text _text = null;
	// Use this for initialization
	void Start () {

		User data = UserModel.Instance.data;
		if (data != null) {
			WebLoaderTask<ResceiveInfo> web = WebAction.Instance.receiveMessage (data.id, data.password);
			web.onSucceed += delegate(ResceiveInfo info) {
				for (int i = 0; i < info.receive.Count; ++i) {
					messages_.Push (info.receive [i].getText ());
				}
			};
			TaskManager.Run (web);
		}
	}
	public void next(){
		_text.text = "";
		
	}
	public void Update(){
		if (String.IsNullOrEmpty (_text.text) && messages_.Count > 0) {
			String message = messages_.Pop ();
			_text.text = message;
		}
	}
}
