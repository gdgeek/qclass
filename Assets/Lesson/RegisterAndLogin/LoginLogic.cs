using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;

class MenuLogic
{
	
	private  FSM fsm_ = null;

	public static string RootState = "menu";

	private State getRoot(){
		State state = new State ();
		return state;

	}

	public static string CheckState = RootState + "_check";
	private State getCheck(){
		State state = TaskState.Create (delegate() {
			return new Task();
		}, this.fsm_, delegate {
			if(UserModel.Instance.hasInfo){
				return LoginState;	
			}else{
				return RegisterInputState;	
			}

		});
	 
		return state;

	}

	public static string LoginState = RootState + "_login";
	private State getLogin(){
		bool error = false;
		State state = TaskState.Create (delegate() {
			//得到用户数据
			User data = UserModel.Instance.data;
			//创建网络任务
			WebLoaderTask<UserInfo> web = WebAction.Instance.login(data.id, data.password);
			//错误处理
			web.onError += delegate(string msg) {
				error = true;
			};
			//成功处理
			web.onSucceed += delegate(UserInfo info) {
				error = false;
			};
			return web;
		}, this.fsm_, delegate {
			if(error){
				//登陆错误，删除用户信息，回到检查状态
				UserModel.Instance.clear();
				return CheckState;
			}else{
				//成功去Start状态
				return StartState;
			}

		});

		return state;
	}


	public static string RegisterInputState = RootState + "_register_input";
	private State getRegisterInput(){

		State state = new State ();
		state.onStart += delegate() {
			//打开注册菜单
			MenuView.Instance._rigister.gameObject.SetActive(true);
			MenuView.Instance._top.gameObject.SetActive(false);
		};

		//当接收到register消息之后
		state.addAction ("register", delegate(FSMEvent evt) {
			
			string error;
			if(UserModel.Instance.checkName(out error)){
				//如果昵称正确，关闭警告，进入联网注册状态
				MenuView.Instance._rigister.warning(null);
				return RegisterWWWState;
			}else{
				//否则，提示错误
				MenuView.Instance._rigister.warning(error);
				Debug.Log(error);

			}
			return "";
		});
		return state;
	}


	public static string RegisterWWWState = RootState + "_register_www";
	private State getRegisterWWW(){
		bool error = true;
		State state = TaskState.Create (delegate() {
			//建立联网任务
			WebLoaderTask<UserInfo> register = WebAction.Instance.register(UserModel.Instance.userName);
			register.onError += delegate(string msg) {
				//如果错误，提示警告
				MenuView.Instance._rigister.warning(msg);

				error = true;
			};
			register.onSucceed += delegate(UserInfo info) {
				//如果正确检查信息是否成功
				if(info.succeed){
					//成功，保存用户信息
					error = false;
					UserModel.Instance.data = info.user;
				}else{
					//失败提示警告
					error = true;
					MenuView.Instance._rigister.warning(info.message);
				}

			};
			TaskList tl = new TaskList();
			//显示Loading界面
			tl.push(Loading.Instance.show(0.5f));
			tl.push(register);
			//关闭Loading界面
			tl.push(Loading.Instance.hide());

			return tl;
		}, fsm_, delegate(FSMEvent evt) {
			if(error){
				//如果错误，回去输入界面
				return RegisterInputState;
			}
			//否则进入游戏开始
			return StartState;
		});

		return state;
	}


	public static string StartState = RootState + "_input";
	private State getStart(){
		
		State state = new State ();
		state.onStart += delegate() {
			//关闭注册窗口
			MenuView.Instance.closeRegister();
			//显示最高排行按钮
			MenuView.Instance.top();
			TaskSet ts = new TaskSet();
			//飞机飞进来
			ts.push(MenuView.Instance._fly.comein ());
			TaskManager.Run(ts);
		};

		state.onOver += delegate() {
			TaskManager.Run(MenuView.Instance._fly.goout ());
		};
		return state;
	}

	 
	private string exit_;
	private string exitTop_;
	public State setup(FSM fsm){
		fsm_ = fsm;
		State root = getRoot ();
		fsm_.addState (RootState, root);
		fsm_.addState (CheckState, getCheck (), RootState);
		fsm_.addState (LoginState, getLogin (), RootState);
		fsm_.addState (RegisterInputState, getRegisterInput (), RootState);
		fsm_.addState (RegisterWWWState, getRegisterWWW (), RootState);
		fsm_.addState (StartState, getStart (), RootState);
		return root;

	}

}


