using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

class TopLogic
{

	private State rootState(){

		bool error = false;
		State state = new State ();
		return state;

	}

	//发送消息
	private State getSendMessage(){

		State state = TaskState.Create (delegate() {
			User data = UserModel.Instance.data;
//			Debug.LogError("++sendMessage+"+ data.id +"=="+ data.password +"=="+ MessageModel.Instance.receiveMessageID +"=="+ MessageModel.Instance.sendMessage);

			WebLoaderTask<MessageInfo> web = WebAction.Instance.sendMessage(data.id,data.password,MessageModel.Instance.receiveMessageID,MessageModel.Instance.sendMessage);
		


			return web;
		}, this.fsm_, delegate {
			return NormalState;

		});

		return state;
	}


	public State showState(){
		State state = TaskState.Create (delegate() {
			WebLoaderTask<TopInfo> web = WebAction.Instance.top10();
			web.onSucceed += delegate(TopInfo info) {

				if(info.top10.Count >0){
					FrinedsData.Instance.setupWarpData(info.top10);
				}

			};

			return web;
		}, this.fsm_, NormalState);
		return state;
	}
	public State normalState(){
		State state = new State ();
		state.addAction ("do_send", SendSate);
		return state;
	}

	public static string RootState = "top";
	public static string ShowState = RootState+"_show";
	public static string SendSate = RootState+"_send";
	public static string NormalState = RootState+"_normal";
	private FSM fsm_ =null;
	public State setup(FSM fsm){
		fsm_ = fsm;
		//排行榜
		//排行榜分数
		State root =  rootState();
		fsm_.addState (RootState, root);
		fsm_.addState (ShowState, showState(), RootState);
		fsm_.addState (NormalState, normalState(), RootState);
		fsm_.addState (SendSate, getSendMessage(), RootState);
		return root;
	}

}

