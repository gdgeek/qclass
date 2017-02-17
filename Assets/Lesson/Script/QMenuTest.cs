using UnityEngine;
using System.Collections;
using GDGeek;

public class QMenuTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TaskList tl = new TaskList ();
		tl.push (new TaskWait(3));
		tl.push (MenuView.Instance.hideRegister ());
		tl.push (MenuView.Instance._fly.comein ());
		tl.push (MenuView.Instance._fly.goout ());
		tl.push (MenuView.Instance._fly.comein ());
		tl.push (MenuView.Instance._fly.goout ());
		tl.push (MenuView.Instance._fly.comein ());
		tl.push (MenuView.Instance._fly.goout ());
		TaskManager.Run (tl);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
