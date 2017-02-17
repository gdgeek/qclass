using UnityEngine;
using System.Collections;

public class NaviManager : MonoBehaviour {


	public Navi _currNavi = null;
	public GameObject _fly = null;
	// Update is called once per frame
	void Update () {
		Navi[] ns =this.transform.parent.GetComponentsInChildren<Navi> ();

		Navi currNavi = null;

		for (int i = 0; i < ns.Length; ++i) {
			if (_fly.transform.position.z < ns [i].transform.position.z) {
				if (currNavi == null) {
					currNavi = ns [i];
				} else {
					if (ns [i].transform.position.z < currNavi.transform.position.z) {
						currNavi = ns [i];
					}
			
				}
			}
		}

		if (currNavi != _currNavi) {
			if (_currNavi != null) {
				_currNavi._isEnable = false;
			}
			if (currNavi != null) {
				currNavi._isEnable = true;
			}
			_currNavi = currNavi;
		}
	}
}
