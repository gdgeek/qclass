using UnityEngine;
using System.Collections;
using GDGeek;
using System;
using System.Collections.Generic;
[Serializable]
public class Top{

	[SerializeField]
	public string id;
	[SerializeField]
	public string score;
 
	[SerializeField]
	public string nickname;
}

public class TopInfo : DataInfo {
	[SerializeField]
	public List<Top> top10;
	 
}
