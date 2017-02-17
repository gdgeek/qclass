using UnityEngine;
using System.Collections;
using GVR.Samples.SkyShip;

public class Navi : MonoBehaviour {
	public SkyShipController _controller;
	public bool _isEnable = false;

	
	// Update is called once per frame
	void Update () {
		if(_isEnable){
		_controller.targetRotation = Quaternion.LookRotation ((this.transform.position - _controller.transform.position).normalized);//
		}
	}
}
