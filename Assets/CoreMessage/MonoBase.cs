using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//接受消息的基类脚本
public class MonoBase : MonoBehaviour
{

    //可以重写
    public virtual void ReceiveMessage(Message message)
    {

    }

    public virtual void ReceiveMessage(Message message, Action<Message> Response)
    {

    }
    
}
