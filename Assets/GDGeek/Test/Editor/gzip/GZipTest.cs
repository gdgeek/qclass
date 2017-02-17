using UnityEngine;
using System.Collections;
using NUnit.Framework;
using ICSharpCode.SharpZipLib;  
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using System;  

public class GZipTest : MonoBehaviour {


	[Test] 
	public void firstTest(){
		MemoryStream ms = new MemoryStream();  
		GZipOutputStream gzip = new GZipOutputStream(ms);  
		byte[] binary = Encoding.UTF8.GetBytes("sddddddddd");  
		gzip.Write(binary, 0, binary.Length);  
		gzip.Close();  
		byte[] press = ms.ToArray();  
		Debug.Log(Convert.ToBase64String(press) + "  " + press.Length);  


		GZipInputStream gzi = new GZipInputStream(new MemoryStream(press));  

		MemoryStream re = new MemoryStream();  
		int count=0;  
		byte[] data=new byte[4096];  
		while ((count = gzi.Read(data, 0, data.Length)) != 0)  
		{  
			re.Write(data,0,count);  
		}  
		byte[] depress = re.ToArray();  
		Debug.Log(Encoding.UTF8.GetString(depress));  
	}
}
