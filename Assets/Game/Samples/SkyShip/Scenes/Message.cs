using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;
public class Message : MonoBehaviour {
 
	public string receiveMessageID_;
	public InputField field;
	public string getMessageText(){
         
		return field.text;
	}
}
