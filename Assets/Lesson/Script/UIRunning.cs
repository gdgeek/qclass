using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIRunning : MonoBehaviour {

	public Sprite[] _sprites;
	private int n_ = 0;
	private float time_ = 0.0f;
	public Image _image;
	// Update is called once per frame
	void Update () {
		time_ += Time.deltaTime;
		if (time_> 0.1f) {
			time_ -= 0.1f;
			n_++;
			_image.sprite = _sprites [n_ % _sprites.Length];
		}
	}
}
