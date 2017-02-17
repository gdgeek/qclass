using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameLogic : Singleton<GameLogic> {
	public Loading _loading;
	private MenuLogic login_;

	private TopLogic top_;
	private GameoverLogic gameover_;

//	private int coin_;
//	private float distance_;

	public void doStart(){
		fsm_.post ("start");
	}
	public void doRegister(){
		fsm_.post ("register");
	}
	public void backMenu(){
		fsm_.post("go_back");
	}
	public void doTop(){
		fsm_.post("go_top");
	}
	public void doSendMessage(){


		fsm_.post("do_send");
	}





	private FSM fsm_ = new FSM();

	private State getPlay(){
		State state = new State ();
		state.addAction ("game_over", "play2over");
		state.onStart += delegate() {
			Task task = new TaskWait(1);
			TaskManager.PushBack(task, delegate() {
				//fsm_.post("game_over");
			});
			TaskManager.Run(task);
		};
		return state;
	}
	private State changeScene(string from, string to, string next){
		State state =  TaskState.Create (delegate() {
			Task task = new Task();
			AsyncOperation a = null;
			TaskManager.PushFront(task, delegate() {
				a = SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);

			});
			task.isOver = delegate() {
				if(a.progress >=1.0f){
					return true;
				}
				return false;
			};
			TaskManager.PushBack(task, delegate() {
				var scene = SceneManager.GetSceneByName(to);
				SceneManager.SetActiveScene (scene);
				SceneManager.UnloadScene(from);
				if(to == "GameOver"){
					GameOver.Instance.setScore(PlayModel.Instance._coin);
					GameOver.Instance.setDistance(PlayModel.Instance._distance);
				}
			});

			TaskList tl = new TaskList();
			tl.push(this._loading.show());
			tl.push(task);
			tl.push(this._loading.hide());

			return tl;
		},fsm_,next);

		return state;
	}
	State getBegin(){
		State state = TaskState.Create (delegate() {

			Task task = new Task();
			AsyncOperation a = null;
			TaskManager.PushFront(task, delegate() {
				a = SceneManager.LoadSceneAsync("QMenu",LoadSceneMode.Additive);

			});
			task.isOver = delegate() {
				if(a.progress >=1.0f){
					return true;
				}
				return false;
			};
			TaskManager.PushBack(task, delegate() {
				var scene = SceneManager.GetSceneByName("QMenu");
				SceneManager.SetActiveScene (scene);
			});

			TaskList tl = new TaskList();
			tl.push(this._loading.show());
			tl.push(task);
			tl.push(this._loading.hide());

			return tl;

		},this.fsm_, MenuLogic.CheckState);
	
		return state;
	}

	// Use this for initialization
	void Start () {
		login_ = new MenuLogic ();
		State loginState = login_.setup (fsm_);
		loginState.addAction ("start", "login2play");
		loginState.addAction ("go_top","menu2top");

		top_ = new TopLogic ();
		State topState = top_.setup (fsm_);
		topState.addAction ("go_back","top2menu");

		gameover_ = new GameoverLogic ();
		State gameoverState = gameover_.setup (fsm_);
		gameoverState.addAction ("reset", "over2menu");

		fsm_.addState ("menu2top", changeScene ("QMenu", "Top", TopLogic.RootState));
		//排行榜返回主菜单
		fsm_.addState ("top2menu",changeScene("Top", "QMenu", MenuLogic.StartState));
		//进入游戏
		fsm_.addState ("login2play", changeScene ("QMenu", "SkyShip", "play"));

		//结束
		fsm_.addState ("play2over",changeScene("SkyShip","GameOver", GameoverLogic.RootState));
		//进入主界面
		fsm_.addState ("over2menu",changeScene ("GameOver", "QMenu", MenuLogic.CheckState));
	


		//等待游戏结束
		fsm_.addState ("play",getPlayGame());
		fsm_.addState ("begin", getBegin());
		fsm_.init ("begin");


	}

	/*
	//查看消息*/



	private State getPlayGame(){
		State state = new State ();
		state.addAction ("game_over","play2over");
		return state;
	}


	public void doGameOver(float distance, int coins){

		PlayModel.Instance._distance = distance;
		PlayModel.Instance._coin = coins;
		fsm_.post ("game_over");

	}
	public void doResetGame(){
		fsm_.post ("reset");
	}
}
