using UnityEngine;
using System.Collections;
using GDGeek;

public class UIFly : MonoBehaviour {
	public Vector3 _down;
	public Vector3 _mid;
	public Vector3 _up;
	public GameObject _fly;
	public Task comein(){
		Task task = new TweenTask (delegate() {
			TweenWorldPosition t = TweenWorldPosition.Begin(_fly, 0.9f, _mid);
			return t;
		});
		TaskManager.PushFront (task, delegate {
			_fly.transform.position = _down;
		});
		return task;
	}

	public Task goout(){
		Task task = new TweenTask (delegate() {
			TweenWorldPosition t = TweenWorldPosition.Begin(_fly, 0.8f, _up);
			return t;
		});
		TaskManager.PushFront (task, delegate {
			_fly.transform.position = _mid;
		});
		return task;
	}
}
