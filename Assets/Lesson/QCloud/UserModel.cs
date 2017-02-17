using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class UserData{
	[SerializeField]
	public string name;
	[SerializeField]
	public string uuid;
	[SerializeField]
	public string password;

}


public class UserModel : GDGeek.Singleton<UserModel> {


	static private UserModel instance_ = null;
	static public UserModel Instance {
		get{
			if (instance_ == null) {
				if (FindObjectsOfType<UserModel>().Length > 1) {
					Debug.LogError ("Single instance"+ typeof(UserModel).Name +" can not have multi instance!!!!");
				}
				instance_ = FindObjectOfType<UserModel> ();
			}
			return instance_;
		}
	}



	private const string key = "user_data";
	private string userName_;
	public string _emptyInfo;


	public User data{
		set{ 
			string json = JsonUtility.ToJson (value);
			PlayerPrefs.SetString (key, json);
			PlayerPrefs.Save ();
		}
		get{ 

			string json = PlayerPrefs.GetString (key);


			return JsonUtility.FromJson<User>(json);
		}

	}
	public void clear(){
		PlayerPrefs.DeleteKey (key);
	}
	public string userName{
		set{ 
			userName_ = value;
		}
		get{ 
			return userName_;
		}

	}

	public bool checkName(out string error){
		if (String.IsNullOrEmpty (userName_)) {
			error = _emptyInfo;
			return false;
		}

		error = null;
		return true;
	}
	//private UserData data_;
	// Use this for initialization
	void Awake(){
		
		//_data = JsonUtility.FromJson<UserData>()
	}
	void Start () {
		//PlayerPrefs.DeleteAll ();
	}

	public bool hasInfo{
		get{ 

			if (PlayerPrefs.HasKey (key)) {

				User data = this.data;
				
				if (string.IsNullOrEmpty (data.id)) {
					return false;
				}
				if (string.IsNullOrEmpty (data.password)) {
					return false;
				}
				return true;
			}
			return false;
		}

	}
	

	public string ResceiveInfo = "ResceiveInfo";

	public ResceiveInfo infoData{
		set{ 
			string json = JsonUtility.ToJson (value);
			Debug.Log (json);
			PlayerPrefs.SetString (ResceiveInfo, json);
			PlayerPrefs.Save ();
		}
		get{ 
			return JsonUtility.FromJson<ResceiveInfo>(PlayerPrefs.GetString (ResceiveInfo));
		}

	}
}
