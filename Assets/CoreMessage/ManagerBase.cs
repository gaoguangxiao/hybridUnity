using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//功能管理基类继承行为
public abstract class ManagerBase
{
    //管理某类脚本
    public List<MonoBase> Monos = new List<MonoBase>();

    //提供注册方法，可让某脚本注册
    public void Register(MonoBase mono)
    {
        //Debug.Log("add monbase:" + mono);
        if (!Monos.Contains(mono))
        {
            Monos.Add(mono);
        }

        foreach (var tmpMone in Monos)
        {
            //查看注册的脚本
            Debug.Log("Register script: " + tmpMone.gameObject);            
        }
        //打印管理类所有的注册脚本
        Debug.Log("Register的数量:" + Monos.Count);
        //LogPlug.Instance.adddLog("Register-Type" + GetMessageType() + ",Monos count:" + Monos.Count);
    }

    public void UnRegister(MonoBase mono)
    {
        //Debug.Log("add monbase:" + mono);
        if (Monos.Contains(mono))
        {
            Monos.Remove(mono);
        }
        //LogPlug.Instance.adddLog("UnRegister-Type" + GetMessageType() + ",Monos count:" + Monos.Count);
    }

    //接受消息`virtual`，可让此方法被重写
    public virtual void ReveiveMessage(Message message)
    {
        //当消息类型不是自己的，不处理
        if (message.Type != GetMessageType())
        {
            return;
        }
        //Debug.Log("消息管理：" + message.Command + message.Type + "管理数量:" + Monos.Count);
        //Debug.Log(message.Content.ToString);
        //消息匹配，处理消息
        //查找匹配的脚本
        //Debug.Log("管理数量:" + Monos.Count);
        foreach (var mono in Monos)
        {
            //Debug.Log("脚本:" + mono);
            mono.ReceiveMessage(message);
        }
    }

    public virtual void ReveiveMessage(Message message, Action<Message> response)
    {
        //Debug.Log("消息管理：" + message.Command + message.Type);
        //当消息类型不是自己的，不处理
        if (message.Type != GetMessageType())
        {
            return;
        }
        foreach (var mono in Monos)
        {
            //查看注册的脚本
            //Debug.Log("script: ", mono);            
            mono.ReceiveMessage(message,response);
        }
    }
    

    //设置当前管理类接受的消息类型
    //`abstract`修饰抽象类和方法，具体实现让子类
    public abstract int GetMessageType();
}
