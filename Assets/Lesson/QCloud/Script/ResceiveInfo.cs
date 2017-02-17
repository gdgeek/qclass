using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using GDGeek;

[Serializable]
public class Sender{

	[SerializeField]
	public string id;

	[SerializeField]
	public string nickname;
}
[Serializable]
public class Resceive{

	[SerializeField]
	public Sender sender;
	[SerializeField]
	public string receiveID;
	[SerializeField]
	public string message;
	public string getText(){
		return sender.nickname + ":" + message;
	}
}
public class ResceiveInfo : DataInfo {

	[SerializeField]
	public List<Resceive> receive;
}
