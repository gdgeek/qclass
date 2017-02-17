using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

class GameoverLogic
{
	public static string RootState = "gameover";
	public static string SubmitState = RootState+"_submit";
	public static string ResettingState = RootState+"_resetting";
	private State getRoot(){
		State state = new State ();
		return state;
	}
	private State getSubmit(){

		State state = TaskState.Create (delegate() {
			User data = UserModel.Instance.data;
			WebLoaderTask<TopInfo> web = WebAction.Instance.submitScore(data.id,data.password, PlayModel.Instance._coin.ToString());
			return web;
		}, this.fsm_, "resetting");

		return state;

	}




	


	private State getResetting(){
		State state = new State();
		return state;

	}
	private FSM fsm_ = null;
	public State setup(FSM fsm){
		fsm_ = fsm;
		State root = getRoot ();
		fsm_.addState (RootState, root);
		//提交分数
		fsm_.addState (SubmitState, getSubmit(),RootState);

		//等待游戏重新开始
		fsm_.addState (ResettingState,getResetting(),RootState);
		return root;
	}
}

