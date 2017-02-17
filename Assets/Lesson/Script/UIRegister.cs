using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;

public class UIRegister : MonoBehaviour {
	public CanvasGroup _cg = null;
	public Text _text = null;
	public void warning(string warning){
		
		if (warning != null) {
			_text.enabled = true;
			_text.text = warning;
		} else {
			_text.enabled = false;
		}

	}

	public Task hide(){
		TweenTask task = new TweenTask
			(
				delegate{
					TweenGroupAlpha alpha = TweenGroupAlpha.Begin(_cg.gameObject,0.15f, 0.0f);
					return alpha;
				}
			);



		TaskManager.PushBack (task, delegate {
			this.gameObject.SetActive(false);
		});
		return task;
	}
}
