using UnityEngine;
using System.Collections;
//using Pathfinding.Serialization.JsonFx;
using GDGeek;
using System;


[Serializable]
public class User{

	[SerializeField]
	public string id;
	[SerializeField]
	public string password;

	[SerializeField]
	public string nickname;
}
public class UserInfo:DataInfo {
	[SerializeField]
	public User user;
}
