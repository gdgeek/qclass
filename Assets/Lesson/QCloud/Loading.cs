using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
using GDGeek;


public class Loading : Singleton<Loading> {
	private GraphicRaycaster gr_ = null; 
	private CanvasGroup cg_ = null;
	void Awake(){
		gr_ = this.GetComponentInChildren<GraphicRaycaster> ();
		gr_.enabled = false;
		cg_ = this.GetComponentInChildren<CanvasGroup> ();
		cg_.alpha = 0;

	}
	public Task show(float alpha = 1.0f){
		TweenTask task = new TweenTask (delegate() {
			
			return TweenGroupAlpha.Begin(cg_.gameObject, 0.3f, alpha);
		});
		TaskManager.PushFront (task, delegate() {
			gr_.enabled = true;
		});
		return task;
	}


	public Task hide(){
		TweenTask task = new TweenTask (delegate() {

			return TweenGroupAlpha.Begin(cg_.gameObject, 0.3f, 0.0f);
		});
		TaskManager.PushFront (task, delegate() {
//			Debug.Log("((())^^^^");
			gr_.enabled = false;
		});

		return task;
	}



	// Use this for initialization
	void Start () {
		/*TaskList tl = new TaskList ();
		tl.push (show ());
		tl.push (hide ());
		tl.push (show ());
		tl.push (hide ());
		TaskManager.Run (tl);*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
