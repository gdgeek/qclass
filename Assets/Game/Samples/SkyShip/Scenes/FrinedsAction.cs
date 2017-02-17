using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FrinedsAction : MonoBehaviour {



	public	void backAction(){

		GameLogic.Instance.backMenu();
	}
	public void sendAction(){

		GameObject panel = GameObject.FindGameObjectWithTag("Panel");
		Message message = panel.gameObject.transform.GetComponentInChildren<Message>(true);
		message.gameObject.SetActive(false);
		Debug.Log ("!!!!!");
		MessageModel.Instance.sendMessage = message.field.text;
		MessageModel.Instance.receiveMessageID =  message.receiveMessageID_;
		GameLogic.Instance.doSendMessage();
		message.field.text = "";

	}

}
