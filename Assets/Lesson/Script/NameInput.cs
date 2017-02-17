using UnityEngine;
using System.Collections;

public class NameInput : MonoBehaviour {
	//public  inpu
	public void onValueChange(){
//		Debug.Log ("change:。");
	
	}
	public void onEndEdit(){
		UnityEngine.UI.InputField input = this.gameObject.GetComponent<UnityEngine.UI.InputField> ();
		if (UserModel.Instance != null) {
			UserModel.Instance.userName = input.text;
		}

	}
		
}
