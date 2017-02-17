using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FrinedsData : Singleton <FrinedsData> {


	private List<Top> listItem_;
	private UIWarpContent warpContent;

	// Use this for initialization
	 
	public void setupWarpData( List<Top> listItem){
		listItem_ = listItem;
		//scrollView 相关所需注意接口
		warpContent = gameObject.transform.GetComponentInChildren<UIWarpContent> ();
		if(warpContent !=null){
			warpContent.onInitializeItem = onInitializeItem;
			//注意：目标init方法必须在warpContent.onInitializeItem之后
			warpContent.Init (listItem.Count);
		}
	}

	private void onInitializeItem(GameObject go,int dataIndex){
 

		Text numberT = go.transform.FindChild ("Number").GetComponent<Text>();
		numberT.text = (dataIndex + 1).ToString();

		Text nameT = go.transform.FindChild ("Name").GetComponent<Text>();
		nameT.text = listItem_[dataIndex].nickname;

		Text scoreT = go.transform.FindChild ("Score").GetComponent<Text>();
		scoreT.text =  listItem_[dataIndex].score;


		//发送按钮监听 
		Button sendButton = go.transform.FindChild ("Button").GetComponent<Button> ();
		sendButton.onClick.RemoveAllListeners();
		sendButton.onClick.AddListener (delegate() {
			string userId = listItem_[dataIndex].id;
			GameObject panel = GameObject.FindGameObjectWithTag("Panel");
			Message message = panel.gameObject.transform.GetComponentInChildren<Message>(true);
			message.gameObject.SetActive(true);
			//接收信息对象的ID
			message.receiveMessageID_ =  userId;
			InputField field = message.gameObject.transform.GetComponentInChildren<InputField>(true);
			message.field =  field;

//			Debug.Log("+++ss+");
		});
	}
}
