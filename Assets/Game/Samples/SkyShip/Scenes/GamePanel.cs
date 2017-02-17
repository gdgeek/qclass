using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
	public GameObject  scoreText_;
	public  GameObject gameALLTime_;
	public  GameObject gameDis_; 
	public  Slider  progress_;
	public int skyShipFlightTime_ = 30;
	private int gameScoreNumber_ = 0;
	private float gameDestance_ = 0;
	private bool gameEndState = false;

	// Use this for initialization
	void Start () {
		if(gameALLTime_ !=  null){
			gameALLTime_.GetComponent<Text>().text = skyShipFlightTime_.ToString();

		}
		if(progress_ !=  null){
			progress_.value = skyShipFlightTime_;

		}

		if(scoreText_ != null )
			scoreText_.GetComponent<Text>().text = gameScoreNumber_.ToString(); 
		if(gameDis_ != null) 
			gameDis_.GetComponent<Text>().text = gameDestance_.ToString(); 
		
		gameEndState = false;
		 
		InvokeRepeating("gameTime",1,1.0f);
	}
	void gameTime(){
		   
		if(skyShipFlightTime_ > 0){
			skyShipFlightTime_--;
			if(gameALLTime_ != null){
				gameALLTime_.GetComponent<Text>().text = skyShipFlightTime_.ToString();
				 
			}

			if(progress_ !=  null){
				progress_.value = skyShipFlightTime_;

			}

		}else if(gameEndState == false && skyShipFlightTime_ == 0 ){
			gameEndState = true;
			Debug.Log("游戏结束"+ skyShipFlightTime_.ToString());

			float distance = gameDestance_;
			int  score =  gameScoreNumber_;
 			GameLogic.Instance.doGameOver( distance ,score);

		}
	}
	public void changeGameTime(int time){ 
		skyShipFlightTime_ += time;
		if(skyShipFlightTime_<0)
			skyShipFlightTime_ = 0;
		if(gameALLTime_ != null ){
			gameALLTime_.GetComponent<Text>().text = skyShipFlightTime_.ToString();
		}
		if(progress_ !=  null){
			progress_.value = skyShipFlightTime_;

		}
	}
	public void changeGameScore(int score){
		gameScoreNumber_ += score;
		if(scoreText_ != null )
			scoreText_.GetComponent<Text>().text = gameScoreNumber_.ToString(); 
	}
	public void changeGameDestance(float destance){
		gameDestance_ += destance;
		if(gameDis_ != null )
			gameDis_.GetComponent<Text>().text = gameDestance_.ToString(); 
	}
	// Update is called once per frame
	void Update () {
	
	}

}
