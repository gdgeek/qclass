using UnityEngine;
using System.Collections;

public class QMenuAction : MonoBehaviour {

	public void doStart(){
	
		GameLogic.Instance.doStart ();
	}
	public void doRegister(){

		GameLogic.Instance.doRegister ();

	}
	public void doTop(){

		GameLogic.Instance.doTop();

	}
	 
}
