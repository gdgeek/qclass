using UnityEngine;
using System.Collections;
using GDGeek;
using System;

public class WebAction : Singleton<WebAction> {

	[Serializable]
	public class WebInfo
	{
		public string url;
		public string info;
	
	}	
	public void Start(){
	//	Debug.Log ("!!!!");
	//	TaskManager.Run( new WebLoaderTask<UserInfo>(_register.url));
	}

	public string _protocol = "http";
	public string _server;
	public string _port = "8080";
	public WebInfo _register;
	public string getUrl(){

		return _protocol +"://" + _server + ":"+_port+"/";
	}
	public WebLoaderTask<UserInfo> register(string nickname){
		var register = new WebLoaderTask<UserInfo>(getUrl()+_register.url);
		register.pack.addField("nickname", nickname);
		return register;
	}

	public WebInfo _login;
	public WebLoaderTask<UserInfo> login(string id, string password){
		var login = new WebLoaderTask<UserInfo>(getUrl()+_login.url);
		login.pack.addField("id", id);
		login.pack.addField("password", password);
		return login;
	}

	public WebInfo _top10;
	public WebLoaderTask<TopInfo> top10(){
		var top10 = new WebLoaderTask<TopInfo>(getUrl()+_top10.url);
		return top10;

	}

	public WebInfo _submitScore;
	public WebLoaderTask<TopInfo> submitScore(string id,string password,string score){

		var submitScore = new WebLoaderTask<TopInfo>(getUrl()+_submitScore.url);
		submitScore.pack.addField("id", id);
		submitScore.pack.addField("password", password);
		submitScore.pack.addField("score", score);
		return submitScore;
	}

	public WebInfo _sendMessage;
	public WebLoaderTask<MessageInfo> sendMessage(string id,string password,string register,string message){

		var sendMessage = new WebLoaderTask<MessageInfo>(getUrl()+_sendMessage.url);
		sendMessage.pack.addField("id", id);
		sendMessage.pack.addField("password", password);
		sendMessage.pack.addField("message", message);
		sendMessage.pack.addField("register", register);

		return sendMessage;
	}

	public WebInfo _receiveMessage;
	public WebLoaderTask<ResceiveInfo> receiveMessage(string id,string password){

		var receiveMessage = new WebLoaderTask<ResceiveInfo>(getUrl()+_receiveMessage.url);
		receiveMessage.pack.addField("id", id);
		receiveMessage.pack.addField("password", password);

		return receiveMessage;
	}
}
