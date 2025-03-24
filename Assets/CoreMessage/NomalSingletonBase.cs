using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//不会被销毁的单例，但只能改挂载一个游戏对象上，挂载
public class NomalSingletonBase<T> : MonoBehaviour where T : NomalSingletonBase<T>
{
    public static T Instance;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        //Debug.Log("Awake:" + gameObject.name);
        //判断当前是否存在实例
        if (Instance != null)
        {
            //Debug.Log("Destroy--:" + gameObject.name);
            //Destroy(gameObject);
            return;
        }
        //Debug.Log("Awake-Instance:" + gameObject.name);
        Instance = this as T;
        //不要销毁当前实例
        DontDestroyOnLoad(gameObject);
    }

    //private static NomalSingletonBase instance;

    //public static NomalSingletonBase Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new NomalSingletonBase();
    //        }
    //        return instance;
    //    }
    //}
}
