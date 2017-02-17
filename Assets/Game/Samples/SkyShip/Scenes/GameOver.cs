using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GDGeek;
public class GameOver : Singleton <GameOver>  {
	public GameObject scoreText_;
	public GameObject distanceText_;

	public void setScore(float score){
		if(scoreText_ != null){
 

			scoreText_.GetComponent<Text>().text = score.ToString();

		}
	}
	public void setDistance(float distance){
		Debug.Log("setDistance");
		if(distanceText_ != null){
			Debug.Log("distanceText_");
			distanceText_.GetComponent<Text>().text = distance.ToString();
		}
	}
	public void resettingGame(){
		Debug.Log("继续");

	}
}
