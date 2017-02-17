using UnityEngine;
using System.Collections;
using SLua;
using UnityEngine.UI;
using System.IO;

public class hotupdate : MonoBehaviour
{
    LuaSvr l;
    public Text logText;
    int progress = 0;

    void log(string cond, string trace, LogType lt)
    {
        logText.text += (cond + "\n");

    }

    void complete()
    {
        l.start("main");
        object o = l.luaState.getFunction("foo").call(1, 2, 3);
        object[] array = (object[])o;
        for (int n = 0; n < array.Length; n++)
            Debug.Log(array[n]);

        string s = (string)l.luaState.getFunction("str").call(new object[0]);
        Debug.Log(s);
    }

    void tick(int p)
    {
        progress = p;
    }

    // Use this for initialization
    void Start()
    {
        path = Application.dataPath + "/SLua/Resources/main.txt";
        excuteBtn.interactable = false;
#if UNITY_5
        Application.logMessageReceived += this.log;
#else
		Application.RegisterLogCallback(this.log);
#endif

    }
    string path;

    public void Download()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        StartCoroutine(loadasset("http://hotupdate-1252759287.costj.myqcloud.com/main.txt"));

    }

    //写入模型到本地
    IEnumerator loadasset(string url)
    {
        WWW w = new WWW(url);
        yield return w;
        if (w.isDone)
        {
            byte[] model = w.bytes;
            int length = model.Length;
            //写入模型到本地
            CreateModelFile(path, model, length);
        }
    }
    void CreateModelFile(string path, byte[] info, int length)
    {
        //文件流信息
        //StreamWriter sw;
        Stream sw;
        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.Create();
        }
        else
        {
            //如果此文件存在则打开
            //sw = t.Append();
            return;
        }
        //以行的形式写入信息
        //sw.WriteLine(info);
        sw.Write(info, 0, length);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
        excuteBtn.interactable = true;
    }
    public Button excuteBtn;
    public void Excute()
    {
        l = new LuaSvr();
        l.init(tick, complete, LuaSvrFlag.LSF_DEBUG);
    }
    void OnGUI()
    {
        if (progress != 100)
            GUI.Label(new Rect(0, 0, 100, 50), string.Format("Loading {0}%", progress));
    }
}
