using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;

public class MenuView : Singleton<MenuView> {
	public UIRegister _rigister = null;
	public TopBtn _top = null;
	public UIFly _fly = null;

	public void register(){
		_rigister.gameObject.SetActive (true);

	}
	public void closeRegister(){
		_rigister.gameObject.SetActive (false);
	}
	public Task hideRegister(){
		return _rigister.hide ();
	}

	public void closeTop(){

		_top.gameObject.SetActive(false);
	}
	public void top(){

		_top.gameObject.SetActive(true);
		this.showName();
	}

	public void showName(){
 
		Name obj = gameObject.GetComponentInChildren<Name>();
		Text nameT = obj.gameObject.transform.GetComponent<Text>();
		 
		nameT.text = UserModel.Instance.data.nickname;
	}
}
