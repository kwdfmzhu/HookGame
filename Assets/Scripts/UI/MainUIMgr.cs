//////////////////////////////////////
// 主UI的事件响应
// UI的事件响应可以直接在unity界面里操作，将绑定的函数对象拖到相应UI的自带事件里，选择对应的函数
// 有些UI并没有相关的响应事件，比如button只有一个onclick，这个时候就需要事件监听MyUGUIEventListener（可能会有冲突）
// 使用事件监听比较容易管理
// 考虑到我们小游戏没有那么复杂，直接界面里拖下好了，没有事件的再使用监听器绑定
// 例如：MyUGUIEventListener.Get(Button.gameObject).onClick = ButtonClickFunction;(c#委托机制)
// by Jimmie
/////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIMgr : MonoBehaviour {
    //private Button m_singleBtn;
    // Use this for initialization
    void Start () {
        //m_singleBtn = gameObject.GetComponent("SingleBtn") as Button;
        /* 有自带的Onclick事件，无需注册，我们小游戏没必要那么严格;
           假如没有自带某个事件，可以使用该监听器（可能会冲突）
        */
         
    }

    // Update is called once per frame
    void Update () {
	
	}

    // 单人游戏
    public void OnSingleBtnClick()
    {
        SceneManager.LoadScene("Fight");
    }

    // 多人联机
    public void OnMultiBtnClick()
    {

    }

    // 退出游戏
    public void OnExitBtnClick()
    {

    }
}
