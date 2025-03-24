using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MC : MySingletonBase<MC>
{
    public static List<ManagerBase> Managers = new List<ManagerBase>();

    //注册管理类
    public void Register(ManagerBase manager)
    {
        //Debug.Log("Register Manager");

        if (!Managers.Contains(manager))
        {
            Managers.Add(manager);
        }

        //LogPlug.Instance.adddLog("Register Manager count:" + Managers.Count);
    }

    //发送消息
    public void SendCustomMessage(Message message)
    {
        //Debug.Log("SendCustomMessage:" + Managers.Count);
        foreach (var manage in Managers)
        {
            manage.ReveiveMessage(message);
        }
    }


    /// <summary>
    /// 发送自定义消息 得到响应
    /// </summary>
    /// <param name="message"></param>
    /// <param name="Response"></param>
    public void SendCustomMessage(Message message, Action<Message> response)
    {
        //Debug.Log("SendCustomMessage:" + Managers.Count);

        foreach (var manage in Managers)
        {
            manage.ReveiveMessage(message,response);
        }
    }
}
